<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor
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
        Me.rtbEditor = New System.Windows.Forms.RichTextBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'rtbEditor
        '
        Me.rtbEditor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbEditor.Location = New System.Drawing.Point(0, 0)
        Me.rtbEditor.Name = "rtbEditor"
        Me.rtbEditor.Size = New System.Drawing.Size(800, 568)
        Me.rtbEditor.TabIndex = 0
        Me.rtbEditor.Text = ""
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(670, 505)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(109, 51)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save and Upload"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 568)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.rtbEditor)
        Me.Name = "frmEditor"
        Me.Text = "frmEditor"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents rtbEditor As RichTextBox
    Friend WithEvents btnSave As Button
End Class
