Imports System.Data.SqlClient
Imports System.Data.Sql
Imports System.Windows.Forms.DataVisualization.Charting
Public Class Form_menu
    Dim da As SqlDataAdapter
    Dim ds As DataSet

    Private Sub chtData()
        konek_db()
        Dim tampil As New SqlCommand("select * from t_barang where kd_barang <> 0", koneksi)
        Dim data As New DataTable
        Dim reader As SqlDataReader
        reader = tampil.ExecuteReader
        data.Load(reader)
        Chart_stok.DataSource = data
        Dim ser As Series = Chart_stok.Series("Stok")
        ser.Name = "Stok"
        Chart_stok.Series(ser.Name).XValueMember = "nama_barang"
        Chart_stok.Series(ser.Name).YValueMembers = "stok"
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_log_out.Click
        Dim message As Integer
        message = MsgBox("Apakah anda yakin akan keluar ?", MsgBoxStyle.YesNo)
        If message = DialogResult.Yes Then
            Form_login.txt_password.ResetText()
            Form_login.txt_usernam.ResetText()
            Form_login.Visible = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_ganti_pass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ganti_pass.Click
        If mLevel = 1 Then
            If Panel3.Visible = False Then
                Panel3.Visible = True
                pnlMasterBarang.Visible = False
                Panel4.Visible = False
            Else
                Panel3.Visible = False
            End If
        Else
            MessageBox.Show("Maaf fitur ini khusus admin", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim message As Integer
        message = MsgBox("Apakah anda yakin akan keluar ?", MsgBoxStyle.YesNo)
        If message = DialogResult.Yes Then
            Form_login.txt_password.ResetText()
            Form_login.txt_usernam.ResetText()
            Form_login.Visible = True
            Me.Close()
        End If
    End Sub

    Private Sub btn_transaksi_Click(sender As Object, e As EventArgs) Handles btn_transaksi.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_transaksi.Show()
    End Sub

    Private Sub lbl_id_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Form_menu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlMasterBarang.Visible = False
        Call chtData()
        Panel3.Visible = False
        Panel4.Visible = False
        lblUID.Text = mNama
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_ganti_password.Show()
    End Sub

    Private Sub btn_brg_masuk_Click(sender As Object, e As EventArgs) Handles btn_brg_masuk.Click
        If mLevel = 1 Then
            If pnlMasterBarang.Visible = False Then
                pnlMasterBarang.Visible = True
                Panel3.Visible = False
                Panel4.Visible = False
            Else
                pnlMasterBarang.Visible = False
            End If
        Else
            MessageBox.Show("Maaf fitur ini khusus admin", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If


    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        If mLevel = 1 Then
            If Panel4.Visible = False Then
                Panel4.Visible = True
                pnlMasterBarang.Visible = False
                Panel3.Visible = False
            Else
                Panel4.Visible = False
            End If
        Else
            MessageBox.Show("Maaf fitur ini khusus admin", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If

    End Sub

    Private Sub btn_man_suplier_Click(sender As Object, e As EventArgs) Handles btn_man_suplier.Click
        If mLevel = 1 Then
            pnlMasterBarang.Visible = False
            Panel3.Visible = False
            Panel4.Visible = False
            form_manajemen_supplier.Show()
        Else
            MessageBox.Show("Maaf fitur ini khusus admin", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Form_kategori.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Form_satuan.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)
        Form_pasok.Show()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)
        Form_manajemen_barang.Show()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs)
        Form_level.Show()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs)
        Form_manajemen_users.Show()
    End Sub

    Private Sub Button4_Click_1(sender As Object, e As EventArgs) 
        Form_kategori.Show()
    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) 
        Form_satuan.Show()
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) 
        Form_pasok.Show()
    End Sub

    Private Sub Button10_Click_1(sender As Object, e As EventArgs) 
        Form_manajemen_barang.Show()
    End Sub

    Private Sub Button12_Click_1(sender As Object, e As EventArgs) 
        Form_level.Show()
    End Sub

    Private Sub Button11_Click_1(sender As Object, e As EventArgs) 
        Form_manajemen_users.Show()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_ecomerce.Show()
    End Sub

    Private Sub Button4_Click_2(sender As Object, e As EventArgs) Handles Button4.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_kategori.Show()
    End Sub

    Private Sub Button5_Click_2(sender As Object, e As EventArgs) Handles Button5.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_satuan.Show()
    End Sub

    Private Sub Button6_Click_2(sender As Object, e As EventArgs) Handles Button6.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_pasok.Show()
    End Sub

    Private Sub Button10_Click_2(sender As Object, e As EventArgs) Handles Button10.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_manajemen_barang.Show()
    End Sub

    Private Sub Button12_Click_2(sender As Object, e As EventArgs) Handles Button12.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_level.Show()
    End Sub

    Private Sub Button11_Click_2(sender As Object, e As EventArgs) Handles Button11.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_manajemen_users.Show()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        LaporanStokBarang.Show()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        LaporanPenjualan.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        LaporanPembelian.Show()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        pnlMasterBarang.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        Form_finansial.Show()
    End Sub

    Private Sub Button14_KeyDown(sender As Object, e As KeyEventArgs) Handles Button14.KeyDown

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
End Class
