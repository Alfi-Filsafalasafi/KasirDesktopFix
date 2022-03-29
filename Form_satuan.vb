Imports System.Data.SqlClient
Imports Microsoft.Office.Interop
Public Class Form_satuan

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_cari.Text = ""
        txt_satuan.Text = ""
        lblID.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_satuan"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)

        dgv.Columns(0).HeaderText = "ID"
        dgv.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgv.Columns(0).Width = "40"
        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_satuan WHERE satuan LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String

        sql = "insert into t_satuan values(@satuan)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@satuan", txt_satuan.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Sukses insert data", "success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
            cmd.Dispose()
        Else
            MessageBox.Show("Gagal insert data", "error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Call clear()
        End If
    End Sub

    Private Sub updateData()
        Call konek_db()
        Dim sql As String

        sql = "UPDATE t_satuan SET satuan=@satuan WHERE id_satuan=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@satuan", txt_satuan.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub deleteData()
        Call konek_db()
        Dim sql As String

        sql = "DELETE FROM t_satuan WHERE id_satuan=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Delete Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Delete Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub btn_simpan_Click(sender As Object, e As EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_satuan FROM t_satuan WHERE id_satuan=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_satuan.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Are you sure?", "Update", MessageBoxButtons.YesNo)
                If dialog = Windows.Forms.DialogResult.No Then
                    Call clear()
                Else
                    Call updateData()
                End If
            End If
        Else
            If txt_satuan.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If lblID.Text = "" Then
            MessageBox.Show("Tidak ada yang di hapus !!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            Dim dialog As DialogResult
            dialog = MessageBox.Show("Are you sure?", "Delete", MessageBoxButtons.YesNo)
            If dialog = Windows.Forms.DialogResult.No Then
                Call clear()
            Else
                Call deleteData()
            End If

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call clear()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        txt_satuan.Text = dgv.Rows(e.RowIndex).Cells(1).Value
    End Sub

    Private Sub Form_satuan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getData()
        lblID.Text = ""
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim xa As New Excel.Application
        Dim ws As Excel.Worksheet
        Dim wb As Excel.Workbook

        wb = xa.Workbooks.Add()
        ws = xa.Worksheets("Sheet1")

        ws.Cells(1, 1) = dgv.Columns(0).HeaderText
        ws.Cells(1, 2) = dgv.Columns(1).HeaderText

        For i As Integer = 0 To dgv.RowCount - 1
            Dim dgvExcel As DataGridViewRow = dgv.Rows(i)

            ws.Cells(2 + i, 1) = dgvExcel.Cells(0).Value
            ws.Cells(2 + i, 2) = dgvExcel.Cells(1).Value

        Next
        xa.Visible = True
    End Sub
End Class