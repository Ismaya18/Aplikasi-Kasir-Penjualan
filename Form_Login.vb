Imports System.Data.OleDb

Public Class Form_Login

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        End

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MessageBox.Show("Username atau Password Masih Kosong!")
        Else
            Call koneksi()
            cmd = New OleDbCommand("select * from tbl_user where user_name='" & TextBox1.Text & "' and pwd='" & TextBox2.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                Form_Menu.lbl_level.Text = rd.Item("lvl")
                Form_Menu.lbl_nama.Text = rd.Item("nama_user")
                Me.Hide()
                Form_Menu.Show()
            Else
                MessageBox.Show("Password atau Username Salah!")
            End If
        End If
    End Sub

    Private Sub Form_Login_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TextBox1.Focus()
    End Sub
End Class