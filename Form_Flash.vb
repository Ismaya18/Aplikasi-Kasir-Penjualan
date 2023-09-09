Public Class Form_Flash

    Private Sub Form_Flash_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ProgressBar1.Value = 0
        Timer1.Enabled = True

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Value += 5
        If ProgressBar1.Value = 100 Then
            Timer1.Dispose()
            Me.Hide()
            Form_Login.Show()
        End If
    End Sub
End Class