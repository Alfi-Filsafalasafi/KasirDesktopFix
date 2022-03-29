Imports System.Data.SqlClient
Public Class form_manajemen_supplier

    Dim cmd As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader

    Sub clear()
        txt_nama.Text = ""
        txt_alamat.Text = ""
        txt_notelp.Text = ""
        txt_email.Text = ""
        lblID.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_supplier"
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

        sql = "SELECT * FROM t_supplier WHERE nama_supplier LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String

        sql = "insert into t_supplier values(@nama_supplier,@alamat,@telp,@email)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@nama_supplier", txt_nama.Text)
        cmd.Parameters.AddWithValue("@alamat", txt_alamat.Text)
        cmd.Parameters.AddWithValue("@telp", txt_notelp.Text)
        cmd.Parameters.AddWithValue("@email", txt_email.Text)

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

        sql = "UPDATE t_supplier SET nama_supplier=@nama_supplier,alamat=@alamat,telp=@telp,email=@email WHERE id_supplier=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@nama_supplier", txt_nama.Text)
        cmd.Parameters.AddWithValue("@alamat", txt_alamat.Text)
        cmd.Parameters.AddWithValue("@telp", txt_notelp.Text)
        cmd.Parameters.AddWithValue("@email", txt_email.Text)

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

        sql = "DELETE FROM t_supplier WHERE id_supplier=@id"
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

    Private Sub txt_notelp_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt_notelp.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[+,0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
            txt_email.Focus()
        End If
    End Sub

    Private Sub txt_notelp_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_notelp.TextChanged

    End Sub

    Private Sub form_manajemen_supplier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblID.Text = ""
        Call getData()
    End Sub

    Private Sub form_manajemen_supplier_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

    End Sub

    Private Sub txt_cari_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub btn_batal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_batal.Click
        Call clear()
    End Sub

    Private Sub btn_hapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_hapus.Click
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

    Private Sub lv_menu_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub

    Private Sub lv_menu_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btn_simpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_supplier FROM t_supplier WHERE id_supplier=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_email.Text = "" Or txt_nama.Text = "" Or txt_notelp.Text = "" Or txt_alamat.Text = "" Then
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
            If txt_email.Text = "" Or txt_nama.Text = "" Or txt_notelp.Text = "" Or txt_alamat.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        txt_nama.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        txt_alamat.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        txt_notelp.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        txt_email.Text = dgv.Rows(e.RowIndex).Cells(4).Value
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub txt_nama_TextChanged(sender As Object, e As EventArgs) Handles txt_nama.TextChanged

    End Sub

    Private Sub txt_nama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nama.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_alamat.Focus()
        End If
    End Sub

    Private Sub txt_alamat_TextChanged(sender As Object, e As EventArgs) Handles txt_alamat.TextChanged

    End Sub

    Private Sub txt_alamat_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_alamat.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_notelp.Focus()
        End If
    End Sub

    Private Sub txt_email_TextChanged(sender As Object, e As EventArgs) Handles txt_email.TextChanged

    End Sub

    Private Sub txt_email_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_email.KeyPress
        If e.KeyChar = Chr(13) Then
            btn_simpan.Focus()
        End If
    End Sub
End Class