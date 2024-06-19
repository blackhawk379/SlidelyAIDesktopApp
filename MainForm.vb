Public Class MainForm
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True
    End Sub
    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        Dim viewForm As New ViewSubmissionsForm()
        viewForm.Show()
    End Sub

    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs) Handles btnCreateSubmission.Click
        Dim createForm As New CreateSubmissionForm()
        createForm.Show()
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub MainForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if CTRL + T is pressed
        If e.Control AndAlso e.KeyCode = Keys.V Then
            ' Trigger the button's click event
            btnViewSubmissions.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            ' Trigger the button's click event
            btnCreateSubmission.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        End If
    End Sub
End Class