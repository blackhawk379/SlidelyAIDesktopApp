﻿Imports Newtonsoft.Json
Imports System.Net.Http

Public Class SearchSubmissionForm
    Private currentIndex As Integer = 0
    Private submissionCount As Integer = 0
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True
    End Sub
    ' Load event handler for the form
    Private Sub SearchSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialization if needed
    End Sub

    ' Function to search for a submission by email
    Private Async Function SearchSubmissionByEmail(email As String) As Task(Of Integer)
        Try
            Dim client As New HttpClient()
            Dim response = Await client.GetAsync($"http://localhost:3000/search?email={email}")

            If response.IsSuccessStatusCode Then
                Dim json = Await response.Content.ReadAsStringAsync()
                Dim data = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)

                Dim index As Integer = Convert.ToInt32(data("index"))
                Dim submission = JsonConvert.DeserializeObject(Of Submission)(JsonConvert.SerializeObject(data("submission")))

                ' Display submission details
                txtName.Text = submission.Name
                txtEmail.Text = submission.Email
                txtPhone.Text = submission.Phone
                txtGithubLink.Text = submission.Github
                txtStopwatchTime.Text = submission.Stopwatch

                Return index ' Return the index for further operations if needed
            Else
                MessageBox.Show("Submission not found")
                Return -1 ' Return -1 or handle the case where submission is not found
            End If
        Catch ex As Exception
            MessageBox.Show($"An error occurred: {ex.Message}")
            Return -1 ' Return -1 or handle the exception case
        End Try
    End Function

    Private Async Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim email As String = txtEmailSearch.Text
        currentIndex = Await SearchSubmissionByEmail(email)
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
    Private Sub SearchSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if CTRL + T is pressed
        If e.Control AndAlso e.KeyCode = Keys.S Then
            ' Trigger the button's click event
            btnSearch.PerformClick()
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
            If currentIndex <= -1 Then
                MessageBox.Show("Please Search and ELement First")
            ElseIf currentIndex > -1 Then
                Dim client As New HttpClient()
                Dim response = Await client.DeleteAsync($"http://localhost:3000/delete?index={currentIndex}")
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission deleted successfully")

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
