<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnCreateSubmission = New System.Windows.Forms.Button()
        Me.btnViewSubmissions = New System.Windows.Forms.Button()
        Me.btnSearchSubmission = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(21, 19)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(441, 26)
        Me.Label6.TabIndex = 41
        Me.Label6.Text = "Shashwat, Slidely Task 2 - Slidely Form App"
        '
        'btnCreateSubmission
        '
        Me.btnCreateSubmission.BackColor = System.Drawing.Color.LightBlue
        Me.btnCreateSubmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCreateSubmission.Location = New System.Drawing.Point(26, 110)
        Me.btnCreateSubmission.Name = "btnCreateSubmission"
        Me.btnCreateSubmission.Size = New System.Drawing.Size(469, 49)
        Me.btnCreateSubmission.TabIndex = 32
        Me.btnCreateSubmission.Text = "CREATE NEW SUBMISSION (CTRL + N)"
        Me.btnCreateSubmission.UseVisualStyleBackColor = False
        '
        'btnViewSubmissions
        '
        Me.btnViewSubmissions.BackColor = System.Drawing.Color.Khaki
        Me.btnViewSubmissions.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnViewSubmissions.Location = New System.Drawing.Point(26, 57)
        Me.btnViewSubmissions.Name = "btnViewSubmissions"
        Me.btnViewSubmissions.Size = New System.Drawing.Size(469, 49)
        Me.btnViewSubmissions.TabIndex = 31
        Me.btnViewSubmissions.Text = "VIEW SUBMISSIONS (CTRL + V)"
        Me.btnViewSubmissions.UseVisualStyleBackColor = False
        '
        'btnSearchSubmission
        '
        Me.btnSearchSubmission.BackColor = System.Drawing.Color.Orange
        Me.btnSearchSubmission.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSearchSubmission.Location = New System.Drawing.Point(26, 165)
        Me.btnSearchSubmission.Name = "btnSearchSubmission"
        Me.btnSearchSubmission.Size = New System.Drawing.Size(469, 49)
        Me.btnSearchSubmission.TabIndex = 42
        Me.btnSearchSubmission.Text = "SEARCH SUBMISSION (CTRL + S)"
        Me.btnSearchSubmission.UseVisualStyleBackColor = False
        '
        'MainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(522, 288)
        Me.Controls.Add(Me.btnSearchSubmission)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnCreateSubmission)
        Me.Controls.Add(Me.btnViewSubmissions)
        Me.Name = "MainForm"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label6 As Label
    Friend WithEvents btnCreateSubmission As Button
    Friend WithEvents btnViewSubmissions As Button
    Friend WithEvents btnSearchSubmission As Button
End Class
