Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class LaporanStokBarang

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,nama_barang,nama_supplier,kategori,satuan,stok,harga_beli,harga_jual,barcode,expired FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "80"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.ReadOnly = True
    End Sub

    Private Sub filterData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT kd_barang,nama_barang,nama_supplier,kategori,satuan,stok,harga_beli,harga_jual,barcode,expired FROM t_barang inner join t_satuan on t_barang.id_satuan = t_satuan.id_satuan inner join t_kategori on t_barang.id_kategori = t_kategori.id_kategori inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier where t_supplier.id_supplier='" & cb_cari.SelectedValue.ToString & "'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "80"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(7).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(9).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.ReadOnly = True
    End Sub

    Private Sub getSupplierCari()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_cari.DataSource = ds.Tables(0)
        cb_cari.ValueMember = "id_supplier"
        cb_cari.DisplayMember = "nama_supplier"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub LaporanStokBarang_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        Call getSupplierCari()
    End Sub

    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        Dim xa As New Excel.Application
        Dim ws As Excel.Worksheet
        Dim wb As Excel.Workbook

        wb = xa.Workbooks.Add()
        ws = xa.Worksheets("Sheet1")

        ws.Cells(1, 1) = dgv.Columns(0).HeaderText
        ws.Cells(1, 2) = dgv.Columns(1).HeaderText
        ws.Cells(1, 3) = dgv.Columns(2).HeaderText
        ws.Cells(1, 4) = dgv.Columns(3).HeaderText
        ws.Cells(1, 5) = dgv.Columns(4).HeaderText
        ws.Cells(1, 6) = dgv.Columns(5).HeaderText
        ws.Cells(1, 7) = dgv.Columns(6).HeaderText
        ws.Cells(1, 8) = dgv.Columns(7).HeaderText
        ws.Cells(1, 9) = dgv.Columns(8).HeaderText
        ws.Cells(1, 10) = dgv.Columns(9).HeaderText

        For i As Integer = 0 To dgv.RowCount - 1
            Dim dgvExcel As DataGridViewRow = dgv.Rows(i)

            ws.Cells(2 + i, 1) = dgvExcel.Cells(0).Value
            ws.Cells(2 + i, 2) = dgvExcel.Cells(1).Value
            ws.Cells(2 + i, 3) = dgvExcel.Cells(2).Value
            ws.Cells(2 + i, 4) = dgvExcel.Cells(3).Value
            ws.Cells(2 + i, 5) = dgvExcel.Cells(4).Value
            ws.Cells(2 + i, 6) = dgvExcel.Cells(5).Value
            ws.Cells(2 + i, 7) = dgvExcel.Cells(6).Value
            ws.Cells(2 + i, 8) = dgvExcel.Cells(7).Value
            ws.Cells(2 + i, 9) = dgvExcel.Cells(8).Value
            ws.Cells(2 + i, 10) = dgvExcel.Cells(9).Value

        Next
        xa.Visible = True
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call filterData()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call getData()
    End Sub
End Class