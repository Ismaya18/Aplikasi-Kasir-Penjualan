Imports System.Data.OleDb
Public Class Form_Input_Barang
    Sub clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        TextBox1.Focus()
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub

    Private Sub Label6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label6.Click

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call clear()
    End Sub

    Private Sub Form_Input_Barang_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call clear()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or ComboBox1.Text = "" Or ComboBox2.Text = "" Then
            MessageBox.Show("Semua Data Wajib Diisi!")
        Else
            'pengecekan data database
            cmd = New OleDbCommand("select * from tbl_barang where kode_barang='" & TextBox1.Text & "'", conn)

            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = False Then
                'penyimpanan data database
                cmd = New OleDbCommand("insert into tbl_barang values('" & TextBox1.Text & _
                                       "','" & TextBox2.Text & _
                                       "','" & ComboBox1.Text & _
                                       "','" & ComboBox2.Text & _
                                       "','" & TextBox3.Text & _
                                       "','" & TextBox4.Text & _
                                       "','" & TextBox5.Text & _
                                       "')", conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data Berhasil Ditambahkan!")
                Call clear()
                Call Form_Barang.tampil_barang()
            Else
                '--edit data
                cmd = New OleDbCommand("update tbl_barang set nama_barang='" & TextBox2.Text & _
                                       "',jenis_barang='" & ComboBox1.Text & _
                                       "',satuan_barang='" & ComboBox2.Text & _
                                       "',harga_beli='" & TextBox3.Text & _
                                       "',harga_jual='" & TextBox4.Text & _
                                       "',stok='" & TextBox5.Text & _
                                       "' where kode_barang='" & TextBox1.Text & "'", conn)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Data Berhasil Diedit!")
                Call clear()
                Call Form_Barang.tampil_barang()
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        'memanggil data
        cmd = New OleDbCommand("select * from tbl_barang where kode_barang='" & TextBox1.Text & "'", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows = True Then
            TextBox2.Text = rd(1)
            ComboBox1.Text = rd(2)
            ComboBox2.Text = rd(3)
            TextBox3.Text = rd(4)
            TextBox4.Text = rd(5)
            TextBox5.Text = rd(6)
        Else
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            TextBox5.Clear()
            ComboBox1.Text = ""
            ComboBox2.Text = ""
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If MessageBox.Show("Apakah Data Akan Dihapus?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            cmd = New OleDbCommand("delete from tbl_barang where kode_barang='" & TextBox1.Text & "'", conn)
            cmd.ExecuteNonQuery()
            MessageBox.Show("Data Berhasil di Hapus!")
            Call clear()
            Call Form_Barang.tampil_barang()
        End If
    End Sub
End Class