Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text

Public Class EditSubmissionForm
    Private stopwatch As New Stopwatch()
    Private WithEvents timerStopwatch As New Timer()
    Private submissionIndex As Integer
    Private previousElapsedTime As TimeSpan

    Public Sub New(index As Integer)
        InitializeComponent()
        Me.KeyPreview = True
        Me.submissionIndex = index

        timerStopwatch.Interval = 1000
    End Sub

    Private Async Sub EditSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim client As New HttpClient()
        Dim response = Await client.GetAsync($"http://localhost:3000/read?index={submissionIndex}")
        If response.IsSuccessStatusCode Then
            Dim json = Await response.Content.ReadAsStringAsync()
            Dim submission = JsonConvert.DeserializeObject(Of Submission)(json)

            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGithubLink.Text = submission.Github
            txtStopwatchTime.Text = submission.Stopwatch

            previousElapsedTime = TimeSpan.Parse(submission.Stopwatch)
            stopwatch.Start()
            txtStopwatchTime.Text = (stopwatch.Elapsed + previousElapsedTime).ToString("hh\:mm\:ss")
        End If

        timerStopwatch.Start()
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Dim client As New HttpClient()
        Dim content As New StringContent(
            $"{{""name"": ""{txtName.Text}"", ""email"": ""{txtEmail.Text}"", ""phone"": ""{txtPhone.Text}"", ""github"": ""{txtGithubLink.Text}"", ""stopwatch"": ""{stopwatch.Elapsed.ToString("hh\:mm\:ss")}""}}",
            Encoding.UTF8, "application/json")

        Dim response = Await client.PutAsync($"http://localhost:3000/update?index={submissionIndex}", content)
        If response.IsSuccessStatusCode Then
            MessageBox.Show("Submission updated successfully")
            Me.Close()

        Else
            MessageBox.Show("Update failed")
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

    Private Sub EditSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
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
        txtStopwatchTime.Text = (stopwatch.Elapsed + previousElapsedTime).ToString("hh\:mm\:ss")
    End Sub

End Class
