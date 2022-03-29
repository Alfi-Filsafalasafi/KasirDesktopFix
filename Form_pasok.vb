Imports System.Data.SqlClient
Imports System.IO
Public Class Form_pasok

    Dim da As SqlDataAdapter
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand

    Private Sub clear()
        txt_cari.Text = ""
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        lblKdBarang.Text = ""
        lblJml.Text = ""
        lblIdSupplier.Text = ""
    End Sub

    Private Sub getData()
        Call konek_db()
        Dim sql As String

        sql = "select id_pasok,nama_barang,nama_supplier,jumlah,tanggal,nama from t_pasok inner join t_barang on t_pasok.kd_barang = t_barang.kd_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier inner join t_users on t_pasok.uid = t_users.uid"
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

        sql = "select id_pasok,nama_barang,nama_supplier,jumlah,tanggal,nama from t_pasok inner join t_barang on t_pasok.kd_barang = t_barang.kd_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier inner join t_users on t_pasok.uid = t_users.uid WHERE nama_barang LIKE '%" & txt_cari.Text & "%'"
        da = New SqlDataAdapter(sql, koneksi)
        ds = New DataSet
        da.Fill(ds, 0)
        dgv.DataSource = ds.Tables(0)
    End Sub

    Private Sub insertData()
        Call konek_db()
        Dim sql As String

        sql = "insert into t_pasok values(@uid,@kd_barang,@jumlah,@tanggal)"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@uid", mUID)
        cmd.Parameters.AddWithValue("@kd_barang", lblKdBarang.Text)
        cmd.Parameters.AddWithValue("@jumlah", TextBox4.Text)
        cmd.Parameters.AddWithValue("@tanggal", lblTanggal.Text)

        If cmd.ExecuteNonQuery Then
            Dim queryU As String
            queryU = "update t_barang set stok += @jumlah where kd_barang=@id"
            cmd = New SqlCommand(queryU, koneksi)
            cmd.Parameters.AddWithValue("id", lblKdBarang.Text)
            cmd.Parameters.AddWithValue("jumlah", TextBox4.Text)

            cmd.ExecuteNonQuery()

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

        sql = "UPDATE t_pasok SET uid=@uid , kd_barang=@kd_barang, jumlah=@jumlah, tanggal=@tanggal WHERE id_pasok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        cmd.Parameters.AddWithValue("@uid", mUID)
        cmd.Parameters.AddWithValue("@kd_barang", lblKdBarang.Text)
        cmd.Parameters.AddWithValue("@jumlah", TextBox4.Text)
        cmd.Parameters.AddWithValue("@tanggal", lblTanggal.Text)

        If cmd.ExecuteNonQuery Then
            Dim stok_update As Integer = Val(TextBox4.Text) - Val(lblJml.Text)
            Dim queryU As String
            queryU = "update t_barang set stok += @jumlah where kd_barang=@id"
            cmd = New SqlCommand(queryU, koneksi)
            cmd.Parameters.AddWithValue("id", lblKdBarang.Text)
            cmd.Parameters.AddWithValue("jumlah", stok_update)

            cmd.ExecuteNonQuery()

            MessageBox.Show("Update Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            dgv.Refresh()
            Call getData()
            Call clear()
        Else
            MessageBox.Show("Update Failed", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Call clear()
        End If
    End Sub

    Private Sub txt_jml_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
        End If
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Form_pasok_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblJml.Text = ""
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox5.Enabled = False
        lblTanggal.Text = System.DateTime.Now.ToString(("yyyy-MM-dd"))
        lblIdSupplier.Text = ""
        lblKdBarang.Text = ""
        lblID.Text = ""
        Call getData()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged

    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Form_cari_barang.Show()
    End Sub

    Private Sub btn_simpan_Click(sender As Object, e As EventArgs) Handles btn_simpan.Click
        Call konek_db()
        Dim sql As String

        sql = "SELECT id_pasok FROM t_pasok WHERE id_pasok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            If lblID.Text = "" Or lblKdBarang.Text = "" Or TextBox4.Text = "" Then
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
            If lblKdBarang.Text = "" Or TextBox4.Text = "" Then
                MessageBox.Show("Lengkapi Data", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Call insertData()
            End If
        End If
    End Sub

    Private Sub btn_batal_Click(sender As Object, e As EventArgs) Handles btn_batal.Click
        Call clear()
    End Sub

    Private Sub txt_cari_TextChanged(sender As Object, e As EventArgs) Handles txt_cari.TextChanged
        Call searchData()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick
        lblID.Text = dgv.Rows(e.RowIndex).Cells(0).Value

        Call konek_db()
        Dim sql As String

        sql = "SELECT * FROM t_pasok inner join t_barang on t_pasok.kd_barang = t_barang.kd_barang inner join t_supplier on t_barang.id_supplier = t_supplier.id_supplier WHERE id_pasok=@id"
        cmd = New SqlCommand(sql, koneksi)
        cmd.Parameters.AddWithValue("@id", lblID.Text)
        dr = cmd.ExecuteReader
        dr.Read()

        If dr.HasRows Then
            lblKdBarang.Text = dr("kd_barang")
            lblIdSupplier.Text = dr("id_supplier")
            TextBox1.Text = dr("barcode")
            TextBox2.Text = dr("nama_supplier")
            TextBox3.Text = dr("nama_barang")
            TextBox4.Text = dr("jumlah")
            TextBox5.Text = dr("alamat")
            lblJml.Text = dr("jumlah")
        Else
            MessageBox.Show("Tidak ada", "errors", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
End Class