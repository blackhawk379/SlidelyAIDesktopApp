Imports Newtonsoft.Json
Imports System.Net.Http

Public Class SearchSubmissionForm

    ' Load event handler for the form
    Private Sub SearchSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization if needed
    End Sub

    ' Function to search for a submission by email
    Private Async Function SearchSubmissionByEmail(email As String) As Task
        Try
            Dim client As New HttpClient()
            Dim response = Await client.GetAsync($"http://localhost:3000/search?email={email}")
            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim submission = JsonConvert.DeserializeObject(Of Submission)(json)

                ' Display submission details
                txtName.Text = submission.Name
                txtEmail.Text = submission.Email
                txtPhone.Text = submission.Phone
                txtGithubLink.Text = submission.Github
                txtStopwatchTime.Text = submission.Stopwatch
            Else
                MessageBox.Show("Submission not found")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Function

    Private Async Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim email As String = txtEmail.Text
        Await SearchSubmissionByEmail(email)
    End Sub
End Class
