Imports System.Data.SqlClient
Public Class Form_manajemen_users
    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_cari.Text = ""
        txt_nama.Text = ""
        txt_password.Text = ""
        txt_telp.Text = ""
        txt_username.Text = ""
        lblID.Text = ""
        cb_level.Text = ""
    End Sub

    Private Sub column()
        dgvEmployee.Columns(0).HeaderText = "ID"
        dgvEmployee.Columns(1).HeaderText = "Nama"
        dgvEmployee.Columns(2).HeaderText = "Username"
        dgvEmployee.Columns(3).HeaderText = "Password"
        dgvEmployee.Columns(4).HeaderText = "Handphone"
        dgvEmployee.Columns(5).HeaderText = "Posisi"

        dgvEmployee.Columns(0).ReadOnly = True
        dgvEmployee.Columns(1).ReadOnly = True
        dgvEmployee.Columns(2).ReadOnly = True
        dgvEmployee.Columns(3).ReadOnly = True
        dgvEmployee.Columns(4).ReadOnly = True
        dgvEmployee.Columns(5).ReadOnly = True

    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT uid,nama,username,password,telp,posisi FROM t_users inner join t_level on t_users.id_level = t_level.id_level"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgvEmployee.DataSource = ds.Tables(0)
        Call column()
        dgvEmployee.Columns(0).Width = "40"
        dgvEmployee.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private Sub getLevel()
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_level,posisi FROM t_level"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        cb_level.DataSource = ds.Tables(0)
        cb_level.ValueMember = "id_level"
        cb_level.DisplayMember = "posisi"
    End Sub

    Private Sub searchData()
        Call konek_db()
        Dim sql As String

        sql = "SELECT uid,nama,username,password,telp,posisi FROM t_users inner join t_level on t_users.id_level = t_level.id_level WHERE nama LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgvEmployee.DataSource = ds.Tables(0)
        Call column()
        dgvEmployee.Columns(0).Width = "40"
        dgvEmployee.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
    End Sub

    Private password As String
    Private Sub encrypt()
        Dim security As New Security.Cryptography.MD5CryptoServiceProvider
        Dim txtPass() As Byte = System.Text.Encoding.ASCII.GetBytes(txt_password.Text)
        Dim hach As String

        txtPass = security.ComputeHash(txtPass)
        For Each bt As Byte In txtPass
            hach &= bt.ToString("x2")
        Next
        password = hach.ToString
    End Sub

    Private Sub insertData()
        Call konek_db()
        Call encrypt()
        Dim sql As String

        sql = "INSERT INTO t_users VALUES(@id_level, @nama, @username, @password, @telp)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id_level", cb_level.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@nama", txt_nama.Text)
        cmd.Parameters.AddWithValue("@username", txt_username.Text)
        cmd.Parameters.AddWithValue("@password", password)
        cmd.Parameters.AddWithValue("@telp", txt_telp.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Insert Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvEmployee.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Insert Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub updateData()
        Call konek_db()
        Call encrypt()
        Dim sql As String

        sql = "UPDATE t_users SET id_level=@id_level, nama=@nama, username=@username, telp=@telp WHERE uid=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@id_level", cb_level.SelectedValue.ToString)
        cmd.Parameters.AddWithValue("@nama", txt_nama.Text)
        cmd.Parameters.AddWithValue("@username", txt_username.Text)
        cmd.Parameters.AddWithValue("@telp", txt_telp.Text)

        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvEmployee.Refresh()
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

        sql = "DELETE FROM t_users WHERE uid=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        If cmd.ExecuteNonQuery Then
            MessageBox.Show("Delete Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgvEmployee.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Delete Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub Form_manajemen_users_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call getLevel()
        Call getData()
        lblID.Text = ""
        txt_nama.Focus()
    End Sub

    Private Sub btn_simpan_Click(sender As Object, e As EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_users WHERE uid=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or txt_nama.Text = "" Or txt_username.Text = "" Or txt_password.Text = "" Or txt_telp.Text = "" Or cb_level.Text = "" Then
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
            If txt_nama.Text = "" Or txt_username.Text = "" Or txt_password.Text = "" Or txt_telp.Text = "" Or cb_level.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub btn_hapus_Click(sender As Object, e As EventArgs) Handles btn_hapus.Click
        If lblID.Text = "" Then
            MessageBox.Show("Tidak ada yang dihapus", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    Private Sub btn_batal_Click(sender As Object, e As EventArgs) Handles btn_batal.Click
        Call clear()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub txt_nama_TextChanged(sender As Object, e As EventArgs) Handles txt_nama.TextChanged

    End Sub

    Private Sub txt_nama_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nama.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_username.Focus()
        End If
    End Sub

    Private Sub txt_username_TextChanged(sender As Object, e As EventArgs) Handles txt_username.TextChanged

    End Sub

    Private Sub txt_username_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_username.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_password.Focus()
        End If
    End Sub

    Private Sub txt_password_TextChanged(sender As Object, e As EventArgs) Handles txt_password.TextChanged

    End Sub

    Private Sub txt_password_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_password.KeyPress
        If e.KeyChar = Chr(13) Then
            txt_telp.Focus()
        End If
    End Sub

    Private Sub cb_level_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cb_level.SelectedIndexChanged
        btn_simpan.Focus()
    End Sub

    Private Sub dgvEmployee_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEmployee.CellContentClick
        lblID.Text = dgvEmployee.Rows(e.RowIndex).Cells(0).Value
        txt_nama.Text = dgvEmployee.Rows(e.RowIndex).Cells(1).Value
        txt_username.Text = dgvEmployee.Rows(e.RowIndex).Cells(2).Value
        txt_password.Text = dgvEmployee.Rows(e.RowIndex).Cells(3).Value
        txt_telp.Text = dgvEmployee.Rows(e.RowIndex).Cells(4).Value
        cb_level.Text = dgvEmployee.Rows(e.RowIndex).Cells(5).Value
        txt_password.Enabled = False

    End Sub
End Class