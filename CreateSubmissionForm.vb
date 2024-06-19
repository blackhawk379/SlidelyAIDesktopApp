Imports System.Net.Http
Imports System.Text
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()
    Private WithEvents timerStopwatch As New Timer()
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True

        timerStopwatch.Interval = 1000
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim client As New HttpClient()
        Dim content As New StringContent(
            $"{{""name"": ""{txtName.Text}"", ""email"": ""{txtEmail.Text}"", ""phone"": ""{txtPhone.Text}"", ""github"": ""{txtGithubLink.Text}"", ""stopwatch"": ""{stopwatch.Elapsed.ToString("hh\:mm\:ss")}""}}",
            Encoding.UTF8, "application/json")

        Dim response = Await client.PostAsync("http://localhost:3000/submit", content)
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission successful")
            Me.Close()
        Else
            MessageBox.Show("Submission failed")
        End If
    End Sub

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            timerStopwatch.Stop()
        Else
            stopwatch.Start()
            timerStopwatch.Start()
        End If
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        stopwatch.Start()
        timerStopwatch.Start()
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if CTRL + T is pressed
        If e.Control AndAlso e.KeyCode = Keys.T Then
            ' Trigger the button's click event
            btnToggleStopwatch.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
            ' Trigger the button's click event
            btnSubmit.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        End If
    End Sub

    Private Sub timerStopwatch_Tick(sender As Object, e As EventArgs) Handles timerStopwatch.Tick
        ' Update the txtStopwatchTime with the current elapsed time
        txtStopwatchTime.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

End Class
