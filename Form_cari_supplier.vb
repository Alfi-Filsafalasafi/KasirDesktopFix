Imports System.Data.SqlClient
Public Class Form_cari_supplier
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand

    Private Sub column()
        dgv.Columns(0).HeaderText = "ID"
        dgv.Columns(1).HeaderText = "Supplier"
        dgv.Columns(2).HeaderText = "Alamat"
        dgv.Columns(3).HeaderText = "Telp"
        dgv.Columns(4).HeaderText = "Email"

        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
        dgv.Columns(2).ReadOnly = True
        dgv.Columns(3).ReadOnly = True
        dgv.Columns(4).ReadOnly = True
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select * from t_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "50"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Call column()
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "select * from t_supplier WHERE nama_supplier LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Call column()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Form_cari_supplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick
        Form_manajemen_barang.lblIdSupplier.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Form_manajemen_barang.txt_supplier.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        Me.Close()
    End Sub
End Class