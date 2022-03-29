Imports System.Data.SqlClient
Module settings
    Public mServer As String
    Public mPrinter As String
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Public Sub getDataServer()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_settings where id=1"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        Form_settings.dgv.DataSource = ds.Tables(0)

        Form_settings.dgv.Columns(0).HeaderText = "ID"
        Form_settings.dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Form_settings.dgv.Columns(0).Width = "40"
        Form_settings.dgv.Columns(0).ReadOnly = True
        Form_settings.dgv.Columns(1).ReadOnly = True
    End Sub

    Public Sub getDataPrinter()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_settings where id=2"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        Form_settings.dgvPrinter.DataSource = ds.Tables(0)

        Form_settings.dgvPrinter.Columns(0).HeaderText = "ID"
        Form_settings.dgvPrinter.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        Form_settings.dgvPrinter.Columns(0).Width = "40"
        Form_settings.dgvPrinter.Columns(0).ReadOnly = True
        Form_settings.dgvPrinter.Columns(1).ReadOnly = True
    End Sub

    Public Sub SettingApl()

        For i As Integer = 0 To Form_settings.dgv.RowCount - 2
            mServer = Form_settings.dgv.Rows(i).Cells(1).Value
        Next

        For i As Integer = 0 To Form_settings.dgvPrinter.RowCount - 2
            mPrinter = Form_settings.dgvPrinter.Rows(i).Cells(1).Value
        Next

        Form_settings.txtPrinter.Text = mPrinter
        Form_settings.txtServer.Text = mServer
    End Sub



End Module
