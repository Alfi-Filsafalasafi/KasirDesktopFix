Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class LaporanPembelian

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select t_supplier.nama_supplier,t_barang.nama_barang,t_pasok.jumlah,t_barang.stok,t_barang.harga_beli, t_pasok.jumlah * t_barang.harga_beli as total,t_pasok.tanggal from t_pasok inner join t_barang on t_pasok.kd_barang = t_barang.kd_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "180"
        dgv.Columns(1).Width = "300"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(2).Width = "60"
        dgv.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(3).Width = "60"
        dgv.Columns(4).Width = "100"
        dgv.Columns(5).Width = "100"
        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(6).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(6).Width = "100"
        dgv.ReadOnly = True
    End Sub

    Private Sub filterData()
        Call konek_db()
        Dim sql As String

        sql = "select t_supplier.nama_supplier,t_barang.nama_barang,t_pasok.jumlah,t_barang.stok,t_barang.harga_beli, t_pasok.jumlah * t_barang.harga_beli as total,t_pasok.tanggal from t_pasok inner join t_barang on t_pasok.kd_barang = t_barang.kd_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier where t_pasok.tanggal >= '" & date_dari.Text & "' and t_pasok.tanggal <= '" & date_sampai.Text & "'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "120"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ReadOnly = True
    End Sub

    Private Sub sumTotal()
        Dim total As Integer = 0
        For i As Integer = 0 To dgv.RowCount - 2
            total += dgv.Rows(i).Cells(5).Value
        Next

        lbl_total.Text = total

    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String

        sql = "insert into t_expenditure values(@tanggal_dari,@tanggal_sampai,@expenditure)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@tanggal_dari", date_dari.Text)
        cmd.Parameters.AddWithValue("@tanggal_sampai", date_sampai.Text)
        cmd.Parameters.AddWithValue("@expenditure", lbl_total.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Sukses insert data", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            date_dari.ResetText()
            date_sampai.ResetText()
            cmd.Dispose()
        Else
            MessageBox.Show("Gagal insert data", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            date_dari.ResetText()
            date_sampai.ResetText()
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub LaporanPembelian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        Call sumTotal()
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

        For i As Integer = 0 To dgv.RowCount - 1
            Dim dgvExcel As DataGridViewRow = dgv.Rows(i)

            ws.Cells(2 + i, 1) = dgvExcel.Cells(0).Value
            ws.Cells(2 + i, 2) = dgvExcel.Cells(1).Value
            ws.Cells(2 + i, 3) = dgvExcel.Cells(2).Value
            ws.Cells(2 + i, 4) = dgvExcel.Cells(3).Value
            ws.Cells(2 + i, 5) = dgvExcel.Cells(4).Value
            ws.Cells(2 + i, 6) = dgvExcel.Cells(5).Value

        Next
        xa.Visible = True
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call getData()
        Call sumTotal()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call filterData()
        Call sumTotal()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim dialog As DialogResult
        dialog = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo)
        If dialog = Windows.Forms.DialogResult.No Then
            date_sampai.ResetText()
            date_dari.ResetText()
        Else
            Call insertData()
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub
End Class