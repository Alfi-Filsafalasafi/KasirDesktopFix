Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class Form_ecomerce

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand

    Private Sub clear()

    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select t_jual.id_transaksi, t_ecomerce.nomor, t_ecomerce_jenis.kategori, t_ecomerce.harga, t_ecomerce.sub_total, t_jual.tanggal from t_ecomerce inner join t_jual on t_ecomerce.id_transaksi = t_jual.id_transaksi inner join t_ecomerce_stok on t_ecomerce.id_stok = t_ecomerce_stok.id_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "120"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ReadOnly = True
    End Sub

    Private Sub filterData()
        Call konek_db()
        Dim sql As String

        sql = "select t_jual.id_transaksi, t_ecomerce.nomor, t_ecomerce_jenis.kategori, t_ecomerce.harga, t_ecomerce.sub_total, t_jual.tanggal from t_ecomerce inner join t_jual on t_ecomerce.id_transaksi = t_jual.id_transaksi inner join t_ecomerce_stok on t_ecomerce.id_stok = t_ecomerce_stok.id_stok inner join t_ecomerce_jenis on t_ecomerce_stok.id_jenis = t_ecomerce_jenis.id_jenis where t_ecomerce_jenis.id_jenis='" & cb_cari.SelectedValue.ToString & "' and t_jual.tanggal >= '" & date_dari.Text & "' and t_jual.tanggal <= '" & date_sampai.Text & "'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
        dgv.Columns(0).Width = "120"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.ReadOnly = True
    End Sub

    Private Sub getKategoriCari()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_ecomerce_jenis"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_cari.DataSource = ds.Tables(0)
        cb_cari.ValueMember = "id_jenis"
        cb_cari.DisplayMember = "kategori"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Form_ecomerce_jenis.Show()
        Form_ecomerce_jenis.txt_kategori.Focus()
    End Sub

    Private Sub Form_ecomerce_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        Call getKategoriCari()
    End Sub

    Private Sub txt_no_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_harga_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_harga_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            Button1.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub cb_kategori_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub cb_kategori_KeyPress(sender As Object, e As KeyPressEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call filterData()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Call getData()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
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

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form_ecomerce_stok.Show()
        Form_ecomerce_stok.txt_nominal.Focus()
    End Sub
End Class