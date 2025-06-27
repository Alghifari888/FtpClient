Imports System.IO

Public Class frmEditor

    ' Properti untuk menyimpan path file temporary
    Private ReadOnly _tempFilePath As String

    ' Constructor yang menerima path file temporary saat form dibuat
    Public Sub New(tempFilePath As String, remoteFileName As String)
        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _tempFilePath = tempFilePath
        Me.Text = $"Editing: {remoteFileName}" ' Atur judul jendela
    End Sub

    Private Sub frmEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Saat form dimuat, baca isi file temporary dan tampilkan di editor
        Try
            rtbEditor.Text = File.ReadAllText(_tempFilePath)
        Catch ex As Exception
            MessageBox.Show($"Gagal membaca file temporary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close() ' Tutup jika gagal membaca file
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        ' Saat tombol save diklik, simpan kembali teks ke file temporary
        Try
            File.WriteAllText(_tempFilePath, rtbEditor.Text)

            ' Atur DialogResult ke OK untuk menandakan penyimpanan berhasil
            Me.DialogResult = DialogResult.OK
            Me.Close() ' Tutup form editor
        Catch ex As Exception
            MessageBox.Show($"Gagal menyimpan perubahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
