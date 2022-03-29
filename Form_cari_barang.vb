Imports System.Data.SqlClient
Imports System.IO
Public Class Form_cari_barang
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub column()
        dgv.Columns(0).HeaderText = "KDBRG"
        dgv.Columns(1).HeaderText = "Nama"
        dgv.Columns(2).HeaderText = "Supplier"
        dgv.Columns(3).HeaderText = "Kategori"
        dgv.Columns(4).HeaderText = "Satuan"
        dgv.Columns(5).HeaderText = "Stok"
        dgv.Columns(6).HeaderText = "HargaBeli"
        dgv.Columns(7).HeaderText = "HargaJual"
        dgv.Columns(8).HeaderText = "Barcode"
        dgv.Columns(9).HeaderText = "Expired"
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight

        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
        dgv.Columns(2).ReadOnly = True
        dgv.Columns(3).ReadOnly = True
        dgv.Columns(4).ReadOnly = True
        dgv.Columns(5).ReadOnly = True
        dgv.Columns(6).ReadOnly = True
        dgv.Columns(7).ReadOnly = True
        dgv.Columns(8).ReadOnly = True
        dgv.Columns(9).ReadOnly = True
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,nama_barang,nama_supplier,kategori,satuan,stok,harga_beli,harga_jual,barcode,expired FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub searchDataBarang()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,nama_barang,nama_supplier,kategori,satuan,stok,harga_beli,harga_jual,barcode,expired FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_barang LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub searchDataSupplier()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,nama_barang,nama_supplier,kategori,satuan,stok,harga_beli,harga_jual,barcode,expired FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE nama_supplier LIKE '%" & txt_cari_supplier.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        Call column()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form_cari_barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
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
        Call konek_db()
        Dim sql As String

        sql = "select * from t_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier where kd_barang=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", dgv.Item(0, e.RowIndex).Value)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Form_pasok.lblKdBarang.Text = dr("kd_barang")
            Form_pasok.lblIdSupplier.Text = dr("id_supplier")
            Form_pasok.TextBox1.Text = dr("barcode")
            Form_pasok.TextBox2.Text = dr("nama_supplier")
            Form_pasok.TextBox3.Text = dr("nama_barang")
            Form_pasok.TextBox5.Text = dr("alamat")
        End If
    End Sub
End Class