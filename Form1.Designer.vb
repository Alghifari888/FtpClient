<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPort = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.txtPass = New System.Windows.Forms.TextBox()
        Me.lblLocalPath = New System.Windows.Forms.Label()
        Me.lsvLocal = New System.Windows.Forms.ListView()
        Me.lblRemotePath = New System.Windows.Forms.Label()
        Me.lsvRemote = New System.Windows.Forms.ListView()
        Me.btnUpload = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.btnDeleteRemote = New System.Windows.Forms.Button()
        Me.btnCreateDir = New System.Windows.Forms.Button()
        Me.progressBar = New System.Windows.Forms.ProgressBar()
        Me.txtLog = New System.Windows.Forms.TextBox()
        Me.btnConnect = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(208, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(35, 16)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Host"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(52, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Port"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(365, 14)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 16)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Username"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(558, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 16)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Password"
        '
        'txtPort
        '
        Me.txtPort.Location = New System.Drawing.Point(89, 11)
        Me.txtPort.Name = "txtPort"
        Me.txtPort.Size = New System.Drawing.Size(100, 22)
        Me.txtPort.TabIndex = 4
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(441, 11)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(100, 22)
        Me.txtUser.TabIndex = 5
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(249, 11)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(100, 22)
        Me.txtHost.TabIndex = 6
        '
        'txtPass
        '
        Me.txtPass.Location = New System.Drawing.Point(631, 11)
        Me.txtPass.Name = "txtPass"
        Me.txtPass.Size = New System.Drawing.Size(100, 22)
        Me.txtPass.TabIndex = 7
        '
        'lblLocalPath
        '
        Me.lblLocalPath.AutoSize = True
        Me.lblLocalPath.Location = New System.Drawing.Point(169, 53)
        Me.lblLocalPath.Name = "lblLocalPath"
        Me.lblLocalPath.Size = New System.Drawing.Size(81, 16)
        Me.lblLocalPath.TabIndex = 8
        Me.lblLocalPath.Text = "lblLocalPath"
        '
        'lsvLocal
        '
        Me.lsvLocal.HideSelection = False
        Me.lsvLocal.Location = New System.Drawing.Point(16, 81)
        Me.lsvLocal.Name = "lsvLocal"
        Me.lsvLocal.Size = New System.Drawing.Size(470, 229)
        Me.lsvLocal.TabIndex = 9
        Me.lsvLocal.UseCompatibleStateImageBehavior = False
        Me.lsvLocal.View = System.Windows.Forms.View.Details
        '
        'lblRemotePath
        '
        Me.lblRemotePath.AutoSize = True
        Me.lblRemotePath.Location = New System.Drawing.Point(688, 53)
        Me.lblRemotePath.Name = "lblRemotePath"
        Me.lblRemotePath.Size = New System.Drawing.Size(96, 16)
        Me.lblRemotePath.TabIndex = 10
        Me.lblRemotePath.Text = "lblRemotePath"
        '
        'lsvRemote
        '
        Me.lsvRemote.HideSelection = False
        Me.lsvRemote.Location = New System.Drawing.Point(492, 81)
        Me.lsvRemote.Name = "lsvRemote"
        Me.lsvRemote.Size = New System.Drawing.Size(494, 229)
        Me.lsvRemote.TabIndex = 11
        Me.lsvRemote.UseCompatibleStateImageBehavior = False
        Me.lsvRemote.View = System.Windows.Forms.View.Details
        '
        'btnUpload
        '
        Me.btnUpload.Location = New System.Drawing.Point(272, 328)
        Me.btnUpload.Name = "btnUpload"
        Me.btnUpload.Size = New System.Drawing.Size(91, 23)
        Me.btnUpload.TabIndex = 12
        Me.btnUpload.Text = "Upload >>"
        Me.btnUpload.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.Location = New System.Drawing.Point(373, 328)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(113, 23)
        Me.btnDownload.TabIndex = 13
        Me.btnDownload.Text = "Download <<"
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'btnDeleteRemote
        '
        Me.btnDeleteRemote.Location = New System.Drawing.Point(492, 328)
        Me.btnDeleteRemote.Name = "btnDeleteRemote"
        Me.btnDeleteRemote.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteRemote.TabIndex = 14
        Me.btnDeleteRemote.Text = "Delete"
        Me.btnDeleteRemote.UseVisualStyleBackColor = True
        '
        'btnCreateDir
        '
        Me.btnCreateDir.Location = New System.Drawing.Point(573, 328)
        Me.btnCreateDir.Name = "btnCreateDir"
        Me.btnCreateDir.Size = New System.Drawing.Size(124, 23)
        Me.btnCreateDir.TabIndex = 15
        Me.btnCreateDir.Text = "Create Folder"
        Me.btnCreateDir.UseVisualStyleBackColor = True
        '
        'progressBar
        '
        Me.progressBar.Location = New System.Drawing.Point(16, 367)
        Me.progressBar.Name = "progressBar"
        Me.progressBar.Size = New System.Drawing.Size(970, 34)
        Me.progressBar.TabIndex = 16
        '
        'txtLog
        '
        Me.txtLog.Location = New System.Drawing.Point(16, 420)
        Me.txtLog.Multiline = True
        Me.txtLog.Name = "txtLog"
        Me.txtLog.Size = New System.Drawing.Size(970, 104)
        Me.txtLog.TabIndex = 17
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(748, 12)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(187, 23)
        Me.btnConnect.TabIndex = 18
        Me.btnConnect.Text = "Connect"
        Me.btnConnect.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(703, 328)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(124, 23)
        Me.btnEdit.TabIndex = 19
        Me.btnEdit.Text = "Edit File"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1042, 535)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtLog)
        Me.Controls.Add(Me.progressBar)
        Me.Controls.Add(Me.btnCreateDir)
        Me.Controls.Add(Me.btnDeleteRemote)
        Me.Controls.Add(Me.btnDownload)
        Me.Controls.Add(Me.btnUpload)
        Me.Controls.Add(Me.lsvRemote)
        Me.Controls.Add(Me.lblRemotePath)
        Me.Controls.Add(Me.lsvLocal)
        Me.Controls.Add(Me.lblLocalPath)
        Me.Controls.Add(Me.txtPass)
        Me.Controls.Add(Me.txtHost)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtPort)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtPort As TextBox
    Friend WithEvents txtUser As TextBox
    Friend WithEvents txtHost As TextBox
    Friend WithEvents txtPass As TextBox
    Friend WithEvents lblLocalPath As Label
    Friend WithEvents lsvLocal As ListView
    Friend WithEvents lblRemotePath As Label
    Friend WithEvents lsvRemote As ListView
    Friend WithEvents btnUpload As Button
    Friend WithEvents btnDownload As Button
    Friend WithEvents btnDeleteRemote As Button
    Friend WithEvents btnCreateDir As Button
    Friend WithEvents progressBar As ProgressBar
    Friend WithEvents txtLog As TextBox
    Friend WithEvents btnConnect As Button
    Friend WithEvents btnEdit As Button
End Class
