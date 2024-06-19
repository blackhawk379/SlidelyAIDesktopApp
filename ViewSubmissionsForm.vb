Imports Newtonsoft.Json
Imports System.Net.Http

Public Class ViewSubmissionsForm
    Private currentIndex As Integer = 0
    Private submissionCount As Integer = 0
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True
    End Sub
    ' Load event handler for the form
    Private Async Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Display the first submission when the form loads
        Await DisplaySubmission(currentIndex)
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        submissionCount = Await GetSubmissionCount()
        If currentIndex < submissionCount - 1 Then
            currentIndex += 1
            Await DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            Await DisplaySubmission(currentIndex)
        End If
    End Sub

    Private Async Function DisplaySubmission(index As Integer) As Task
        Try
            Dim client As New HttpClient()
            Dim response = Await client.GetAsync($"http://localhost:3000/read?index={index}")
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
                MessageBox.Show("Error fetching submission")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Function

    ' A method to get the total count of submissions
    ' This could be optimized based on your actual data handling
    Private Async Function GetSubmissionCount() As Task(Of Integer)
        Try
            Dim client As New HttpClient()
            Dim response = Await client.GetAsync("http://localhost:3000/count")
            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim result = JsonConvert.DeserializeObject(Of Dictionary(Of String, Integer))(json)
                Return result("count")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
        Return 0
    End Function

    ' Handle the KeyDown event
    Private Sub ViewSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if CTRL + T is pressed
        If e.Control AndAlso e.KeyCode = Keys.N Then
            ' Trigger the button's click event
            btnNext.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
            ' Trigger the button's click event
            btnPrevious.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.D Then
            ' Trigger the button's click event
            btnDelete.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.E Then
            ' Trigger the button's click event
            btnEdit.PerformClick()
            ' Mark the event as handled
            e.Handled = True
        End If
    End Sub

    Private Async Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            Dim client As New HttpClient()
            Dim response = Await client.DeleteAsync($"http://localhost:3000/delete?index={currentIndex}")
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission deleted successfully")

                ' Fetch the updated submission count
                submissionCount = Await GetSubmissionCount()

                ' Adjust the currentIndex if needed
                If currentIndex >= submissionCount Then
                    currentIndex -= 1
                End If

                ' Display the next/previous submission if any exist
                If submissionCount > 0 Then
                    Await DisplaySubmission(currentIndex)
                Else
                    ' Clear the display if no submissions are left
                    ClearSubmissionDisplay()
                End If
            Else
                MessageBox.Show("Error deleting submission")
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
        End Try
    End Sub


    Private Sub ClearSubmissionDisplay()
        txtName.Text = ""
        txtEmail.Text = ""
        txtPhone.Text = ""
        txtGithubLink.Text = ""
        txtStopwatchTime.Text = ""
    End Sub

    Private Async Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim editForm As New EditSubmissionForm(currentIndex)
        editForm.ShowDialog()
        ' Optionally refresh the current display after editing
        Await DisplaySubmission(currentIndex)
    End Sub
End Class