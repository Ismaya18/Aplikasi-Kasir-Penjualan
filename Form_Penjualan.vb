Imports System.Data.OleDb
Public Class Form_Penjualan
    Sub no_faktur()
        cmd = New OleDbCommand("select * from tbl_jual where faktur_jual in(select max(faktur_jual) from tbl_jual) order by faktur_jual DESC", conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If Not rd.HasRows Then
            v0nofaktur.Text = Format(Now, "yyMMdd") + "0001"
        Else
            If Microsoft.VisualBasic.Left(rd.GetString(0), 6) <> Format(Now, "yyMMdd") Then
                v0nofaktur.Text = Format(Now, "yyMMdd") + "0001"
            Else
                v0nofaktur.Text = rd.Item("faktur_jual") + 1
            End If
        End If
    End Sub
    Private Sub GroupBox3_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub Label2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label2.Click

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Sub grand_total()
        Dim jumlah As Decimal = 0
        For i As Integer = 0 To DataGridView1.Rows.Count - 1
            jumlah = jumlah + DataGridView1.Rows(i).Cells(6).Value
            v0grantotal.Text = jumlah
        Next
        If v0grantotal.Text = "" Then
            v0grantotal.Text = "0"
        End If
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0kembalian.TextChanged

    End Sub

    Private Sub Label5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label5.Click

    End Sub

    Private Sub Form_Penjualan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()
        Call no_faktur()
        v0kodebarang.Focus()
        v0dibayar.Text = "0"
        v0kembalian.Text = "0"
        Call grand_total()
        v0kasir.Text = Form_Menu.lbl_nama.Text
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0dibayar.TextChanged
        Try
            v0kembalian.Text = Val(v0dibayar.Text) - Val(v0grantotal.Text)
        Catch ex As Exception
            v0kembalian.Text = "0"
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0namabarang.TextChanged

    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0harga.TextChanged

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        v0tanggal.Text = Format(Now, "dd/MM/yyyy")
        v0jam.Text = Format(Now, "HH:mm:ss")
    End Sub

    Private Sub v0kodebarang_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles v0kodebarang.KeyPress
        If e.KeyChar = Chr(13) Then
            cmd = New OleDbCommand("select * from tbl_barang where kode_barang='" & v0kodebarang.Text & "'", conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If rd.HasRows = True Then
                v0namabarang.Text = rd.Item("nama_barang")
                v0satuan.Text = rd.Item("satuan_barang")
                v0jenisbarang.Text = rd.Item("jenis_barang")
                v0harga.Text = rd.Item("harga_jual")
                v0qty.Focus()
            Else
                v0namabarang.Text = ""
                v0satuan.Text = ""
                v0jenisbarang.Text = ""
                v0harga.Text = ""
                v0kodebarang.Focus()
                MessageBox.Show("Barang Ini Tidak Ditemukan / Tidak Terdapat di Database!")
            End If
        End If
    End Sub
    Sub bersih_barang()
        v0kodebarang.Text = ""
        v0namabarang.Text = ""
        v0satuan.Text = ""
        v0jenisbarang.Text = ""
        v0harga.Text = ""
        v0qty.Text = ""
        v0total.Text = ""
        v0kodebarang.Focus()
    End Sub
    Private Sub v0qty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0qty.TextChanged
        Try
            v0total.Text = Val(v0harga.Text) * Val(v0qty.Text)
        Catch ex As Exception
            v0total.Text = ""
        End Try
    End Sub

    Private Sub v0qty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles v0qty.KeyPress
        If e.KeyChar = Chr(13) Then
            DataGridView1.Rows.Add(v0kodebarang.Text, v0namabarang.Text, v0jenisbarang.Text, v0satuan.Text, v0harga.Text, v0qty.Text, v0total.Text)
            Call bersih_barang()
            Call grand_total()

        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If v0grantotal.Text = "0" Then
            MessageBox.Show("Belum Ada Barang Yang Diinput!")
        ElseIf Val(v0dibayar.Text) < Val(v0grantotal.Text) Then
            MessageBox.Show("Pembayaran Masih Kurang!")
        Else
            'simpan data tabel barang
            Call koneksi()
            cmd = New OleDbCommand("insert into tbl_jual(faktur_jual,tgl_jual,jam,grand_total,dibayar,kembalian,kasir)values('" & v0nofaktur.Text & _
                                   "','" & v0tanggal.Text & _
                                   "','" & v0jam.Text & _
                                   "','" & v0grantotal.Text & _
                                   "','" & v0dibayar.Text & _
                                   "','" & v0kembalian.Text & _
                                   "','" & v0kasir.Text & _
                                   "')", conn)
            cmd.ExecuteNonQuery()
            'simpan data ke tbl rinci jual
            For baris As Integer = 0 To DataGridView1.Rows.Count - 2
                cmd = New OleDbCommand("insert into tbl_rinci_jual values('" & v0nofaktur.Text & _
                                       "','" & DataGridView1.Rows(baris).Cells(0).Value & _
                                       "','" & DataGridView1.Rows(baris).Cells(5).Value & _
                                       "','" & DataGridView1.Rows(baris).Cells(6).Value & _
                                       "')", conn)
                cmd.ExecuteNonQuery()
                'pengurangan stok
                cmd = New OleDbCommand("select * from tbl_barang where kode_barang='" & DataGridView1.Rows(baris).Cells(0).Value & "'", conn)
                rd = cmd.ExecuteReader
                rd.Read()
                If rd.HasRows = True Then
                    cmd = New OleDbCommand("update tbl_barang set stok='" & rd.Item("stok") - Val(DataGridView1.Rows(baris).Cells(5).Value) & _
                                           "' where kode_barang='" & DataGridView1.Rows(baris).Cells(0).Value & "'", conn)
                    cmd.ExecuteNonQuery()

                End If
            Next
            'bersihkan data transaksi
            MessageBox.Show("Transaksi Penjualan Berhasil Disimpan!")
            DataGridView1.Rows.Clear()
            v0grantotal.Text = "0"
            v0dibayar.Text = "0"
            v0kembalian.Text = "0"
            Call no_faktur()
            Call bersih_barang()
        End If
    End Sub

    Private Sub DataGridView1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DataGridView1.KeyPress
        If e.KeyChar = Chr(27) Then
            Dim baris As Integer
            baris = DataGridView1.CurrentCell.RowIndex
            Try
                DataGridView1.Rows.RemoveAt(baris)
                Call grand_total()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub v0tanggal_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles v0tanggal.TextChanged

    End Sub
End Class