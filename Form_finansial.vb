Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class Form_finansial

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim cmd As SqlCommand

    Private Sub getIncome()
        Call konek_db()
        Dim sql As String

        sql = "select tanggal_dari,tanggal_sampai,income from t_income"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgvIncome.DataSource = ds.Tables(0)
        dgvIncome.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvIncome.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvIncome.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvIncome.ReadOnly = True
    End Sub

    Private Sub sumTotalInc()
        Dim total As Integer = 0
        For i As Integer = 0 To dgvIncome.RowCount - 2
            total += dgvIncome.Rows(i).Cells(2).Value
        Next

        lbl_totalInc.Text = total

    End Sub

    Private Sub getExpenditure()
        Call konek_db()
        Dim sql As String

        sql = "select tanggal_dari,tanggal_sampai,expenditure from t_expenditure"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgvExpenditure.DataSource = ds.Tables(0)
        dgvExpenditure.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvExpenditure.Columns(1).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvExpenditure.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvExpenditure.ReadOnly = True
    End Sub

    Private Sub sumTotalExp()
        Dim total As Integer = 0
        For i As Integer = 0 To dgvExpenditure.RowCount - 2
            total += dgvExpenditure.Rows(i).Cells(2).Value
        Next

        lbl_totalExp.Text = total

    End Sub

    Private Sub Form_finansial_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getExpenditure()
        Call getIncome()
        Call sumTotalExp()
        Call sumTotalInc()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub btn_export_Click(sender As Object, e As EventArgs) Handles btn_export.Click
        Dim xa As New Excel.Application
        Dim ws As Excel.Worksheet
        Dim wb As Excel.Workbook

        wb = xa.Workbooks.Add()
        ws = xa.Worksheets("Sheet1")

        ws.Cells(1, 1) = dgvExpenditure.Columns(0).HeaderText
        ws.Cells(1, 2) = dgvExpenditure.Columns(1).HeaderText
        ws.Cells(1, 3) = dgvExpenditure.Columns(2).HeaderText

        For i As Integer = 0 To dgvExpenditure.RowCount - 1
            Dim dgvExcel As DataGridViewRow = dgvExpenditure.Rows(i)

            ws.Cells(2 + i, 1) = dgvExcel.Cells(0).Value
            ws.Cells(2 + i, 2) = dgvExcel.Cells(1).Value
            ws.Cells(2 + i, 3) = dgvExcel.Cells(2).Value

        Next
        xa.Visible = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim xa As New Excel.Application
        Dim ws As Excel.Worksheet
        Dim wb As Excel.Workbook

        wb = xa.Workbooks.Add()
        ws = xa.Worksheets("Sheet1")

        ws.Cells(1, 1) = dgvIncome.Columns(0).HeaderText
        ws.Cells(1, 2) = dgvIncome.Columns(1).HeaderText
        ws.Cells(1, 3) = dgvIncome.Columns(2).HeaderText

        For i As Integer = 0 To dgvIncome.RowCount - 1
            Dim dgvExcel As DataGridViewRow = dgvIncome.Rows(i)

            ws.Cells(2 + i, 1) = dgvExcel.Cells(0).Value
            ws.Cells(2 + i, 2) = dgvExcel.Cells(1).Value
            ws.Cells(2 + i, 3) = dgvExcel.Cells(2).Value

        Next
        xa.Visible = True
    End Sub
End Class