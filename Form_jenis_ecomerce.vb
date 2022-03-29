Imports System.Data.SqlClient
Public Class Form_jenis_ecomerce

    Dim da As SqlDataAdapter
    Dim ds As DataSet

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select id_stok,kategori,nominal,harga_beli,harga_jual,stok,t_ecomerce_jenis.id_jenis from t_ecomerce_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "50"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "select id_stok,t_ecomerce_jenis.kategori,nominal,harga_beli,harga_jual,stok,t_ecomerce_jenis.id_jenis from t_ecomerce_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis where t_ecomerce_jenis.kategori Like '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "50"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ReadOnly = True
    End Sub

    Private Sub Form_jenis_ecomerce_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        Form_transaksi.Enabled = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form_transaksi.Enabled = True
        Me.Close()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        Form_transaksi.lblIDEcomerce.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Form_transaksi.lblJenisEcomerce.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        Form_transaksi.txt_jml_ecomerce.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        Form_transaksi.lblStokEcomerce.Text = dgv.Rows(e.RowIndex).Cells(5).Value
        Form_transaksi.lblBiayaEcomerce.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        Form_transaksi.txt_harga_ecomerce.Text = dgv.Rows(e.RowIndex).Cells(4).Value
        Form_transaksi.lblJenisID.Text = dgv.Rows(e.RowIndex).Cells(6).Value
        Form_transaksi.Enabled = True
        Me.Close()
        Form_transaksi.txt_nomor.Focus()
    End Sub
End Class