Imports System.Net
Imports System.IO

Public Class Form1

    ' Variabel untuk menyimpan informasi kredensial FTP
    Private ftpHost As String
    Private ftpUser As String
    Private ftpPass As String
    Private ftpPort As Integer

    '================================================================================
    ' EVENT HANDLERS (Fungsi yang terhubung dengan aksi di Form)
    '================================================================================

    ' --- Event yang dijalankan saat Form pertama kali dimuat ---
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' -- Inisialisasi Tampilan Awal --
        ' Atur teks default untuk koneksi (opsional, untuk testing)
        txtHost.Text = "ftp.dlptest.com"
        txtPort.Text = "21"
        txtUser.Text = "dlpuser"
        txtPass.Text = "rNrKYTX9g7z3RgJk"
        txtPass.PasswordChar = "*" ' Sembunyikan karakter password

        ' -- Konfigurasi Kolom untuk ListView --
        ' ListView Lokal
        lsvLocal.View = View.Details
        lsvLocal.Columns.Add("Name", 200)
        lsvLocal.Columns.Add("Size", 80)
        lsvLocal.Columns.Add("Date Modified", 120)

        ' ListView Remote
        lsvRemote.View = View.Details
        lsvRemote.Columns.Add("Name", 200)
        lsvRemote.Columns.Add("Size", 80)
        lsvRemote.Columns.Add("Type", 80)

        ' Muat drive lokal sebagai titik awal dan beri pesan log
        LoadLocalDrives()
        LogActivity("Aplikasi siap. Silakan isi detail FTP dan klik Connect.")
    End Sub

    ' --- Event saat item di ListView Lokal di-double-click (navigasi folder) ---
    Private Sub lsvLocal_DoubleClick(sender As Object, e As EventArgs) Handles lsvLocal.DoubleClick
        If lsvLocal.SelectedItems.Count = 0 Then Return

        Dim selectedItem = lsvLocal.SelectedItems(0)
        Dim currentPath = lblLocalPath.Text

        If selectedItem.Tag.ToString() = "D:" Then ' Jika yang diklik adalah Drive atau Folder
            If currentPath = "My Computer" Then
                LoadLocalDirectory(selectedItem.Text)
            Else
                LoadLocalDirectory(Path.Combine(currentPath, selectedItem.Text))
            End If
        ElseIf selectedItem.Tag.ToString() = "UP" Then ' Jika ".." (untuk naik folder) diklik
            Dim parentDir = Directory.GetParent(currentPath)
            If parentDir IsNot Nothing Then
                LoadLocalDirectory(parentDir.FullName)
            Else
                LoadLocalDrives() ' Jika sudah di root, kembali ke daftar drive
            End If
        End If
    End Sub

    ' --- Event saat item di ListView Remote di-double-click (navigasi folder) ---
    Private Async Sub lsvRemote_DoubleClick(sender As Object, e As EventArgs) Handles lsvRemote.DoubleClick
        If lsvRemote.SelectedItems.Count = 0 Then Return

        Dim selectedItem = lsvRemote.SelectedItems(0)
        Dim currentPath = lblRemotePath.Text.TrimEnd("/"c)
        Dim itemName = selectedItem.Text

        If selectedItem.Tag.ToString() = "D:" Then ' Navigasi ke sub-folder
            Dim newPath As String = $"{currentPath}/{itemName}"
            Await ListRemoteDirectoryAsync(newPath)
        ElseIf selectedItem.Tag.ToString() = "UP" Then ' Navigasi ke folder atas
            If currentPath.Contains("/"c) Then
                Dim lastSlashIndex = currentPath.LastIndexOf("/"c)
                Dim parentPath = currentPath.Substring(0, lastSlashIndex)
                If String.IsNullOrEmpty(parentPath) Then parentPath = "/"
                Await ListRemoteDirectoryAsync(parentPath)
            Else
                Await ListRemoteDirectoryAsync("/") ' Jika sudah di root, kembali ke root
            End If
        End If
    End Sub

    ' --- Tombol untuk memulai koneksi ke server FTP ---
    Private Async Sub btnConnect_Click(sender As Object, e As EventArgs) Handles btnConnect.Click
        If String.IsNullOrWhiteSpace(txtHost.Text) OrElse String.IsNullOrWhiteSpace(txtPort.Text) Then
            LogActivity("Error: Host dan Port tidak boleh kosong.")
            Return
        End If

        ' Simpan kredensial dari TextBox
        ftpHost = txtHost.Text
        ftpUser = txtUser.Text
        ftpPass = txtPass.Text
        Integer.TryParse(txtPort.Text, ftpPort)

        LogActivity($"Menghubungkan ke ftp://{ftpHost}:{ftpPort}...")
        btnConnect.Enabled = False ' Nonaktifkan tombol selama proses koneksi

        ' Coba untuk me-list direktori root sebagai tes koneksi
        Await ListRemoteDirectoryAsync("/")

        btnConnect.Enabled = True ' Aktifkan kembali tombol setelah selesai
    End Sub

    ' --- Tombol untuk meng-upload file ---
    Private Async Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If lsvLocal.SelectedItems.Count = 0 OrElse lsvLocal.SelectedItems(0).Tag.ToString() <> "F:" Then
            LogActivity("Pilih sebuah file dari panel lokal untuk di-upload.")
            Return
        End If

        Dim localFileItem = lsvLocal.SelectedItems(0)
        Dim localFilePath As String = Path.Combine(lblLocalPath.Text, localFileItem.Text)

        Dim remoteFileName As String = localFileItem.Text
        Dim remotePath = lblRemotePath.Text.TrimEnd("/"c)

        Await UploadFileAsync(localFilePath, remotePath, remoteFileName)
        Await ListRemoteDirectoryAsync(remotePath) ' Refresh tampilan remote
    End Sub

    ' --- Tombol untuk men-download file ---
    Private Async Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        If lsvRemote.SelectedItems.Count = 0 OrElse lsvRemote.SelectedItems(0).Tag.ToString() <> "F:" Then
            LogActivity("Pilih sebuah file dari panel server untuk di-download.")
            Return
        End If

        Dim remoteFileItem = lsvRemote.SelectedItems(0)
        Dim remoteFileName = remoteFileItem.Text
        Dim localDownloadPath As String = lblLocalPath.Text

        ' Pastikan path lokal valid dan bukan "My Computer"
        If localDownloadPath = "My Computer" OrElse Not Directory.Exists(localDownloadPath) Then
            LogActivity("Error: Pilih folder tujuan yang valid di panel lokal.")
            Return
        End If

        Await DownloadFileAsync(remoteFileName, localDownloadPath)
        LoadLocalDirectory(localDownloadPath) ' Refresh tampilan lokal
    End Sub


    ' --- Tombol untuk menghapus file atau folder di remote server ---
    Private Async Sub btnDeleteRemote_Click(sender As Object, e As EventArgs) Handles btnDeleteRemote.Click
        If lsvRemote.SelectedItems.Count = 0 Then
            LogActivity("Pilih file atau folder di server untuk dihapus.")
            Return
        End If

        Dim selectedItem = lsvRemote.SelectedItems(0)
        Dim itemName = selectedItem.Text
        Dim currentPath = lblRemotePath.Text.TrimEnd("/"c)
        Dim itemFullPath As String = $"{currentPath}/{itemName}"

        If MessageBox.Show($"Apakah Anda yakin ingin menghapus '{itemName}' dari server?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
            Return
        End If

        LogActivity($"Menghapus '{itemName}'...")
        Try
            Dim request As FtpWebRequest
            If selectedItem.Tag.ToString() = "F:" Then ' Jika yang dipilih adalah file
                request = CreateFtpRequest($"ftp://{ftpHost}:{ftpPort}{itemFullPath}", WebRequestMethods.Ftp.DeleteFile)
            Else ' Jika yang dipilih adalah direktori
                request = CreateFtpRequest($"ftp://{ftpHost}:{ftpPort}{itemFullPath}", WebRequestMethods.Ftp.RemoveDirectory)
            End If

            Using response As FtpWebResponse = CType(Await request.GetResponseAsync(), FtpWebResponse)
                LogActivity($"Delete successful: {response.StatusDescription}")
            End Using

            ' Refresh tampilan remote
            Await ListRemoteDirectoryAsync(currentPath)
        Catch ex As Exception
            LogActivity($"Delete Error: {ex.Message}")
        End Try
    End Sub

    ' --- Tombol untuk membuat folder baru di server ---
    Private Async Sub btnCreateDir_Click(sender As Object, e As EventArgs) Handles btnCreateDir.Click
        Dim folderName As String = InputBox("Masukkan nama folder baru:", "Buat Folder")
        If String.IsNullOrWhiteSpace(folderName) Then Return

        Dim currentPath = lblRemotePath.Text.TrimEnd("/"c)
        Dim newFolderPath As String = $"{currentPath}/{folderName}"

        LogActivity($"Membuat folder '{folderName}'...")
        Try
            Dim request As FtpWebRequest = CreateFtpRequest($"ftp://{ftpHost}:{ftpPort}{newFolderPath}", WebRequestMethods.Ftp.MakeDirectory)
            Using response As FtpWebResponse = CType(Await request.GetResponseAsync(), FtpWebResponse)
                LogActivity($"Folder created: {response.StatusDescription}")
            End Using

            ' Refresh tampilan remote
            Await ListRemoteDirectoryAsync(currentPath)
        Catch ex As Exception
            LogActivity($"Create Directory Error: {ex.Message}")
        End Try
    End Sub

    ' --- ### BARU: Tombol untuk mengedit file di server ### ---
    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If lsvRemote.SelectedItems.Count = 0 OrElse lsvRemote.SelectedItems(0).Tag.ToString() <> "F:" Then
            LogActivity("Pilih sebuah file dari panel server untuk diedit.")
            Return
        End If

        Dim remoteFileItem = lsvRemote.SelectedItems(0)
        Dim remoteFileName = remoteFileItem.Text

        ' Download file ke folder temporary sistem
        Dim tempLocalPath As String = Path.GetTempPath()
        Dim tempFilePath As String = Path.Combine(tempLocalPath, remoteFileName)

        ' Download file untuk diedit
        Dim downloadSuccess As Boolean = Await DownloadFileAsync(remoteFileName, tempLocalPath, "edit")
        If Not downloadSuccess Then
            LogActivity($"Gagal membuka file '{remoteFileName}' untuk diedit.")
            Return
        End If

        ' Buka form editor
        Using editorForm As New frmEditor(tempFilePath, remoteFileName)
            ' Tampilkan form editor dan tunggu sampai ditutup
            If editorForm.ShowDialog() = DialogResult.OK Then
                ' Jika user menekan "Save", upload file kembali ke server
                LogActivity("Menyimpan perubahan kembali ke server...")
                Dim remotePath = lblRemotePath.Text.TrimEnd("/"c)
                Await UploadFileAsync(tempFilePath, remotePath, remoteFileName)
            Else
                LogActivity("Proses edit dibatalkan oleh pengguna.")
            End If
        End Using

        ' Selalu hapus file temporary setelah selesai, baik disimpan maupun tidak
        Try
            If File.Exists(tempFilePath) Then
                File.Delete(tempFilePath)
                LogActivity($"File temporary '{remoteFileName}' telah dihapus dari lokal.")
            End If
        Catch ex As Exception
            LogActivity($"Gagal menghapus file temporary: {ex.Message}")
        End Try

    End Sub


    '================================================================================
    ' FUNGSI PEMBANTU (Helper Methods)
    '================================================================================

#Region "Manajemen Log & UI Helpers"

    ' --- Prosedur untuk mencatat aktivitas ke TextBox Log (Thread-Safe) ---
    Public Sub LogActivity(message As String)
        If txtLog.InvokeRequired Then
            ' Jika dipanggil dari thread lain, gunakan Invoke untuk keamanan
            txtLog.Invoke(New Action(Of String)(AddressOf LogActivity), message)
            Return
        End If

        Dim timestamp As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        txtLog.AppendText($"[{timestamp}] {message}{vbCrLf}")
        txtLog.ScrollToCaret() ' Auto-scroll ke bawah
    End Sub

    ' --- Prosedur untuk mengupdate ProgressBar (Thread-Safe) ---
    Private Sub UpdateProgress(value As Integer)
        If progressBar.InvokeRequired Then
            progressBar.Invoke(New Action(Of Integer)(AddressOf UpdateProgress), value)
            Return
        End If
        ' Pastikan value berada dalam rentang 0-100
        If value < progressBar.Minimum Then value = progressBar.Minimum
        If value > progressBar.Maximum Then value = progressBar.Maximum
        progressBar.Value = value
    End Sub

#End Region

#Region "Manajemen File & Folder Lokal"

    ' --- Memuat semua drive yang tersedia di komputer lokal ---
    Private Sub LoadLocalDrives()
        lsvLocal.Items.Clear()
        lblLocalPath.Text = "My Computer"
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            If drive.IsReady Then
                Dim item As New ListViewItem(drive.Name)
                item.SubItems.Add("") ' Size
                item.SubItems.Add("Drive")
                item.Tag = "D:" ' Tandai sebagai drive/direktori
                lsvLocal.Items.Add(item)
            End If
        Next
    End Sub

    ' --- Memuat file dan folder dari path lokal tertentu ---
    Private Sub LoadLocalDirectory(path As String)
        Try
            lsvLocal.Items.Clear()
            lblLocalPath.Text = path

            ' Selalu tambahkan item ".." untuk navigasi ke atas.
            Dim upItem As New ListViewItem("..")
            upItem.SubItems.Add("")
            upItem.SubItems.Add("Parent Directory")
            upItem.Tag = "UP"
            lsvLocal.Items.Add(upItem)

            ' Muat semua direktori
            For Each dir As String In Directory.GetDirectories(path)
                Dim dirInfo As New DirectoryInfo(dir)
                Dim item As New ListViewItem(dirInfo.Name)
                item.SubItems.Add("<DIR>")
                item.SubItems.Add(dirInfo.LastWriteTime.ToString())
                item.Tag = "D:" ' Tandai sebagai direktori
                lsvLocal.Items.Add(item)
            Next

            ' Muat semua file
            For Each file As String In Directory.GetFiles(path)
                Dim fileInfo As New FileInfo(file)
                Dim item As New ListViewItem(fileInfo.Name)
                item.SubItems.Add($"{Math.Ceiling(fileInfo.Length / 1024.0)} KB") ' Ukuran dalam KB
                item.SubItems.Add(fileInfo.LastWriteTime.ToString())
                item.Tag = "F:" ' Tandai sebagai file
                lsvLocal.Items.Add(item)
            Next

        Catch ex As Exception
            LogActivity($"Error reading local directory: {ex.Message}")
            ' Jika ada error (misal: access denied), coba kembali ke direktori parent
            Dim parent As DirectoryInfo = Directory.GetParent(path)
            If parent IsNot Nothing Then
                LoadLocalDirectory(parent.FullName)
            Else
                LoadLocalDrives()
            End If
        End Try
    End Sub

#End Region

#Region "Koneksi & Operasi FTP"

    ' --- Membuat objek FtpWebRequest dengan kredensial standar ---
    Private Function CreateFtpRequest(uriString As String, method As String) As FtpWebRequest
        Dim request As FtpWebRequest = CType(WebRequest.Create(uriString), FtpWebRequest)
        request.Credentials = New NetworkCredential(ftpUser, ftpPass)
        request.Method = method
        ' --- Sesuai permintaan: Gunakan Passive Mode & Binary Transfer ---
        request.UsePassive = True
        request.UseBinary = True
        request.KeepAlive = False ' Nonaktifkan keep-alive untuk menghindari timeout pada beberapa server
        Return request
    End Function

    ' --- Fungsi Asynchronous untuk me-list file dan folder di server FTP ---
    Private Async Function ListRemoteDirectoryAsync(remotePath As String) As Task
        lsvRemote.Items.Clear()
        lblRemotePath.Text = remotePath
        LogActivity($"Listing directory: {remotePath}")

        Try
            Dim uriString As String = $"ftp://{ftpHost}:{ftpPort}{remotePath}"
            Dim request As FtpWebRequest = CreateFtpRequest(uriString, WebRequestMethods.Ftp.ListDirectoryDetails)

            Using response As FtpWebResponse = CType(Await request.GetResponseAsync(), FtpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using reader As New StreamReader(responseStream)
                        Dim directoryListing As String = Await reader.ReadToEndAsync()
                        response.Close()

                        ' Parsing hasil dari ListDirectoryDetails
                        Dim lines As String() = directoryListing.Split({vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

                        ' Tambahkan item ".." untuk navigasi ke atas
                        If remotePath <> "/" Then
                            Dim upItem As New ListViewItem("..")
                            upItem.SubItems.Add("")
                            upItem.SubItems.Add("Parent")
                            upItem.Tag = "UP"
                            lsvRemote.Items.Add(upItem)
                        End If

                        For Each line As String In lines
                            ' Parsing untuk format list FTP style Unix yang umum
                            Dim parts As String() = line.Split(New Char() {" "c}, 9, StringSplitOptions.RemoveEmptyEntries)
                            If parts.Length < 9 Then Continue For ' Skip baris yang tidak valid

                            Dim name As String = parts(8)
                            Dim size As String = parts(4)
                            Dim isDirectory As Boolean = line.StartsWith("d")

                            Dim item As New ListViewItem(name)
                            item.SubItems.Add(If(isDirectory, "<DIR>", $"{Math.Ceiling(Long.Parse(size) / 1024.0)} KB"))
                            item.SubItems.Add(If(isDirectory, "Folder", "File"))
                            item.Tag = If(isDirectory, "D:", "F:")

                            lsvRemote.Items.Add(item)
                        Next
                        LogActivity($"Success: Directory listed. ({response.StatusDescription})")
                    End Using
                End Using
            End Using
        Catch ex As WebException
            Dim response As FtpWebResponse = CType(ex.Response, FtpWebResponse)
            If response IsNot Nothing Then
                LogActivity($"FTP Error: {response.StatusCode} - {response.StatusDescription}")
            Else
                LogActivity($"FTP Error: {ex.Message}")
            End If
        Catch ex As Exception
            LogActivity($"General Error: {ex.Message}")
        End Try
    End Function

    ' --- Fungsi untuk meng-upload file ke server ---
    Private Async Function UploadFileAsync(localFilePath As String, remotePath As String, remoteFileName As String) As Task
        Dim remoteUri As String = $"ftp://{ftpHost}:{ftpPort}{remotePath}/{remoteFileName}"

        LogActivity($"Uploading {remoteFileName} to {remotePath}/...")
        UpdateProgress(0)

        Try
            Dim request As FtpWebRequest = CreateFtpRequest(remoteUri, WebRequestMethods.Ftp.UploadFile)
            Dim fileInfo As New FileInfo(localFilePath)
            request.ContentLength = fileInfo.Length

            Const bufferSize As Integer = 4096
            Dim buffer(bufferSize - 1) As Byte
            Dim bytesRead As Integer
            Dim totalBytesRead As Long = 0

            Using localFileStream As FileStream = fileInfo.OpenRead()
                Using requestStream As Stream = Await request.GetRequestStreamAsync()
                    Do
                        bytesRead = Await localFileStream.ReadAsync(buffer, 0, bufferSize)
                        If bytesRead = 0 Then Exit Do

                        Await requestStream.WriteAsync(buffer, 0, bytesRead)
                        totalBytesRead += bytesRead

                        Dim progressPercentage As Integer = CInt((totalBytesRead * 100) / fileInfo.Length)
                        UpdateProgress(progressPercentage)
                    Loop
                End Using
            End Using

            Using response As FtpWebResponse = CType(Await request.GetResponseAsync(), FtpWebResponse)
                LogActivity($"Upload complete: {response.StatusDescription}")
            End Using
            UpdateProgress(100)
        Catch ex As Exception
            LogActivity($"Upload Error: {ex.Message}")
        Finally
            UpdateProgress(0)
        End Try
    End Function

    ' --- Fungsi untuk men-download file dari server ---
    Private Async Function DownloadFileAsync(remoteFileName As String, localDownloadPath As String, Optional mode As String = "download") As Task(Of Boolean)
        Dim remotePath = lblRemotePath.Text.TrimEnd("/"c)
        Dim remoteUri As String = $"ftp://{ftpHost}:{ftpPort}{remotePath}/{remoteFileName}"
        Dim localFilePath As String = Path.Combine(localDownloadPath, remoteFileName)

        If mode = "download" Then
            LogActivity($"Downloading {remoteFileName} to {localDownloadPath}...")
        Else
            LogActivity($"Downloading temporary file {remoteFileName} for editing...")
        End If

        UpdateProgress(0)

        Try
            Dim sizeRequest As FtpWebRequest = CreateFtpRequest(remoteUri, WebRequestMethods.Ftp.GetFileSize)
            Dim fileSize As Long
            Using sizeResponse As FtpWebResponse = CType(Await sizeRequest.GetResponseAsync(), FtpWebResponse)
                fileSize = sizeResponse.ContentLength
                sizeResponse.Close()
            End Using

            Dim downloadRequest As FtpWebRequest = CreateFtpRequest(remoteUri, WebRequestMethods.Ftp.DownloadFile)

            Using response As FtpWebResponse = CType(Await downloadRequest.GetResponseAsync(), FtpWebResponse)
                Using responseStream As Stream = response.GetResponseStream()
                    Using localFileStream As New FileStream(localFilePath, FileMode.Create)
                        Const bufferSize As Integer = 4096
                        Dim buffer(bufferSize - 1) As Byte
                        Dim bytesRead As Integer
                        Dim totalBytesRead As Long = 0

                        Do
                            bytesRead = Await responseStream.ReadAsync(buffer, 0, bufferSize)
                            If bytesRead = 0 Then Exit Do

                            Await localFileStream.WriteAsync(buffer, 0, bytesRead)
                            totalBytesRead += bytesRead

                            Dim progressPercentage As Integer = CInt((totalBytesRead * 100) / fileSize)
                            UpdateProgress(progressPercentage)
                        Loop
                    End Using
                End Using
                If mode = "download" Then
                    LogActivity($"Download complete: {response.StatusDescription}")
                End If
            End Using
            UpdateProgress(100)
            Return True ' Download berhasil
        Catch ex As Exception
            LogActivity($"Download Error: {ex.Message}")
            Return False ' Download gagal
        Finally
            UpdateProgress(0)
        End Try
    End Function

    Private Sub Label5_Click(sender As Object, e As EventArgs) Handles Label5.Click

    End Sub

    Imports System.Diagnostics

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Try
            Process.Start(New ProcessStartInfo With {
                .FileName = "https://github.com/alghifari888",
                .UseShellExecute = True
            })
        Catch ex As Exception
            MessageBox.Show("Gagal membuka link: " & ex.Message)
        End Try
    End Sub



#End Region

End Class
