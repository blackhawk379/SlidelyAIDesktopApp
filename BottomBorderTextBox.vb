Public Class BottomBorderTextBox
    Inherits TextBox

    Private borderColor As Color = Color.Black ' Adjust the color as needed

    Public Sub New()
        MyBase.New()
        Me.BorderStyle = BorderStyle.None
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' Draw the bottom border
        Dim pen As New Pen(borderColor)
        e.Graphics.DrawLine(pen, 0, Me.Height - 1, Me.Width, Me.Height - 1)
    End Sub
End Class
