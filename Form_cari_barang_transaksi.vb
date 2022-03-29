Imports System.Data.SqlClient
Imports System.IO
Public Class Form_cari_barang_transaksi

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub column()
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgv.Columns(0).Width = "50"
        dgv.Columns(2).Width = "250"
        dgv.Columns(3).Width = "50"
        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(4).Width = "120"
        dgv.Columns(5).Width = "40"
        dgv.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 10)


        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
        dgv.Columns(2).ReadOnly = True
        dgv.Columns(3).ReadOnly = True
        dgv.Columns(4).ReadOnly = True
        dgv.Columns(5).ReadOnly = True
        dgv.Columns(6).ReadOnly = True
        dgv.Columns(7).ReadOnly = True
        dgv.Columns(8).ReadOnly = True
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual,nama_supplier FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub searchDataBarang()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual,nama_supplier FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_barang LIKE '%" & txt_cari.Text & "%' or kd_barang LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub searchDataSupplier()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,barcode,nama_barang,satuan,kategori,stok,harga_beli,harga_jual,nama_supplier FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_supplier LIKE '%" & txt_cari_supplier.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub Form_cari_barang_transaksi_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        Form_transaksi.Enabled = False
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchDataBarang()
    End Sub

    Private Sub txt_cari_supplier_TextChanged(sender As Object, e As EventArgs) Handles txt_cari_supplier.TextChanged
        Call searchDataSupplier()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick

        Form_transaksi.lblKDBarang.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Form_transaksi.txt_barcode.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        Form_transaksi.txt_nama.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        Form_transaksi.txt_harga.Text = dgv.Rows(e.RowIndex).Cells(7).Value
        Form_transaksi.lblStok.Text = dgv.Rows(e.RowIndex).Cells(5).Value
        Form_transaksi.Enabled = True
        Me.Close()
        Form_transaksi.txt_jml.Focus()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form_transaksi.Enabled = True
        Me.Close()
    End Sub

    Private Sub txt_cari_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_cari.KeyPress
        If e.KeyChar = Chr(13) Then
            dgv.Focus()
        End If
    End Sub

    Private Sub txt_cari_supplier_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_cari_supplier.KeyPress
        If e.KeyChar = Chr(13) Then
            dgv.Focus()
        End If
    End Sub

    Private Sub dgv_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellClick
        If e.RowIndex >= 0 Then
            Form_transaksi.lblKDBarang.Text = dgv.Rows(e.RowIndex).Cells(0).Value
            Form_transaksi.txt_barcode.Text = dgv.Rows(e.RowIndex).Cells(1).Value
            Form_transaksi.txt_nama.Text = dgv.Rows(e.RowIndex).Cells(2).Value
            Form_transaksi.txt_harga.Text = dgv.Rows(e.RowIndex).Cells(7).Value
            Form_transaksi.lblStok.Text = dgv.Rows(e.RowIndex).Cells(5).Value
            Form_transaksi.Enabled = True
            Me.Close()
            Form_transaksi.txt_jml.Focus()
        End If
    End Sub
End Class