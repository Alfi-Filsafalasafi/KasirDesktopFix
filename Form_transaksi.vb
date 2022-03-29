Imports System.Data.SqlClient
Imports System.IO
Public Class Form_transaksi
    Dim reader As SqlDataReader
    Dim kd_brng, idku As String
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader


    'print
    Dim TextToPrint As String = ""
    Dim jumlahkarakter As Integer = 50 '40 adalah jumlah karakter (lebar) yang ada pada struk


    Public Sub PrintHeader()
        Dim huruf As Integer = 0
        Dim sisa As Integer = 0

        Dim NamaKasir As Integer

        TextToPrint = ""

        'Header Struk Nama toko, alamat, telepon
        Dim StringToPrint As String = "ADEN MINI MARKET"
        Dim LineLen As Integer = StringToPrint.Length
        Dim spcLen3 As New String(" "c, Math.Round((jumlahkarakter - LineLen) / 8)) 'This line is used to center text in the middle of the receipt
        TextToPrint &= spcLen3 & StringToPrint & Environment.NewLine

        StringToPrint = "Jl Rengganis Desa Kembang"
        LineLen = StringToPrint.Length
        Dim spcLen1 As New String(" "c, Math.Round((jumlahkarakter - LineLen) / 12))
        TextToPrint &= spcLen3 & StringToPrint & Environment.NewLine

        StringToPrint = "No. Telp : 085233211500"
        LineLen = StringToPrint.Length
        Dim spcLen2 As New String(" "c, Math.Round((jumlahkarakter - LineLen) / 10))
        TextToPrint &= spcLen3 & StringToPrint & Environment.NewLine

        ' garis
        Dim spcLen4 As New String(StrDup(62, "-"))
        TextToPrint &= spcLen4 & Environment.NewLine

        'No Struk dan nama kasir
        Dim no_pesan As String = txt_no_pesan.Text
        huruf = Len(no_pesan) + 6
        NamaKasir = Len(Strings.Left(mNama, 12)) + 8
        sisa = (40 - (huruf + NamaKasir))
        Dim spcLen4b As New String("Bon : " + no_pesan + StrDup(sisa, " ") + "Kasir : " + Strings.Left(mNama, 12))
        TextToPrint &= spcLen4b & Environment.NewLine

        ' garis
        Dim spcLen5 As New String(StrDup(62, "-"))
        TextToPrint &= spcLen5 & Environment.NewLine
    End Sub

    Public Sub ItemsToBePrinted()

        Dim hurufnama As Integer = 0
        Dim sisanama As Integer = 0

        Dim hurufqty As Integer = 0
        Dim sisaqty As Integer = 0

        Dim hurufharga As Integer = 0
        Dim sisaharga As Integer = 0

        Dim hurufsub As Integer = 0
        Dim sisasub As Integer = 0

        Dim hurufdiskon As Integer = 0
        Dim sisadiskon As Integer = 0


        Dim spcLen4bs As New String("   qty            " + "harga         " + "subtotal       " + "diskon")
        TextToPrint &= spcLen4bs & Environment.NewLine

        Dim spcLen5s As New String(StrDup(62, "-"))
        TextToPrint &= spcLen5s & Environment.NewLine

        For i As Integer = 0 To dgv.RowCount - 2
            Dim barang As String = dgv.Item(1, i).Value
            Dim qty As Integer = dgv.Item(2, i).Value
            Dim harga As Integer = dgv.Item(3, i).Value
            Dim sub_total As Integer = harga * qty
            Dim diskon As Integer = dgv.Item(6, i).Value

            hurufnama = Len(Strings.Left(barang, 17))
            sisanama = (25 - hurufnama)
            hurufqty = Len(Format(qty, "#,###"))
            sisaqty = (5 - hurufqty)
            hurufharga = Len(Format(harga, "#,###"))
            sisaharga = (15 - hurufharga)
            hurufsub = Len(Format(sub_total, "#,###"))
            sisasub = (15 - hurufsub)
            hurufdiskon = Len(Format(diskon, "#,###"))
            sisadiskon = (15 - hurufdiskon)

            Dim spcLen21 As New String(Strings.Left(barang, 17))
            TextToPrint &= spcLen21 & Environment.NewLine
            Dim spcLen4b As New String(StrDup(sisaqty, " ") + Format(qty, "#,###") + " x" + StrDup(sisaharga, " ") + Format(harga, "#,###") + StrDup(sisasub, " ") + Format(sub_total, "#,###") + StrDup(sisadiskon, " ") + Format(diskon, "#,###"))
            TextToPrint &= spcLen4b & Environment.NewLine
        Next

    End Sub

    Public Sub ItemsEcomerce()

        Dim hurufnama As Integer = 0
        Dim sisanama As Integer = 0

        Dim hurufqty As Integer = 0
        Dim sisaqty As Integer = 0

        Dim hurufharga As Integer = 0
        Dim sisaharga As Integer = 0

        Dim hurufsub As Integer = 0
        Dim sisasub As Integer = 0
        For i As Integer = 0 To dgvEcomerce.RowCount - 2
            Dim jenis As String = dgvEcomerce.Item(1, i).Value
            Dim hargaE As Integer = dgvEcomerce.Item(3, i).Value
            Dim sub_totalE As Integer = hargaE
            Dim nomor As String = dgvEcomerce.Item(7, i).Value

            hurufnama = Len(Strings.Left(jenis, 17))
            sisanama = (5 - hurufnama)
            hurufharga = Len(Format(hargaE, "#,###"))
            sisaharga = (22 - hurufharga)
            hurufsub = Len(Format(sub_totalE, "#,###"))
            sisasub = (15 - hurufsub)

            Dim spcLen22 As New String(Strings.Left(jenis, 17) + " : " + Strings.Left(nomor, 17))
            TextToPrint &= spcLen22 & Environment.NewLine
            Dim spcLen4b As New String(StrDup(sisanama, " ") + StrDup(sisaharga, " ") + Format(hargaE, "#,###") + StrDup(sisasub, " ") + Format(sub_totalE, "#,###"))
            TextToPrint &= spcLen4b & Environment.NewLine
        Next
    End Sub

    Public Sub printFooter()
        Dim huruf As Integer = 0
        Dim sisa As Integer = 0

        Dim spcLen6 As New String(StrDup(62, "-"))
        TextToPrint &= spcLen6 & Environment.NewLine

        Dim totals As Integer = lbl_total.Text
        huruf = Len(Format(totals, "#,###")) + 5
        sisa = (40 - (huruf))
        Dim spcLen7 As New String("Total" + StrDup(sisa, " ") + Format(totals, "#,###"))
        TextToPrint &= spcLen7 & Environment.NewLine

        Dim bayars As Integer = txt_bayar.Text
        huruf = Len(Format(bayars, "#,###")) + 5
        sisa = (39 - (huruf))
        Dim spcLen10 As New String("Tunai" + StrDup(sisa, " ") + Format(bayars, "#,###"))
        TextToPrint &= spcLen10 & Environment.NewLine

        Dim kembalis As Integer = txt_kembalian.Text
        huruf = Len(Format(kembalis, "#,###")) + 5
        sisa = (36 - (huruf))
        Dim spcLen11 As New String("Kembali" + StrDup(sisa, " ") + Format(kembalis, "#,###"))
        TextToPrint &= spcLen11 & Environment.NewLine

        Dim diskons As Integer = lblDiskon.Text
        huruf = Len(Format(totals, "#,###")) + 5
        sisa = (39 - (huruf))
        Dim spcLen7s As New String("Diskon" + StrDup(sisa, " ") + Format(diskons, "#,###"))
        TextToPrint &= spcLen7s & Environment.NewLine

        Dim spcLen13 As New String(StrDup(62, "-"))
        TextToPrint &= spcLen13 & Environment.NewLine

        Dim yy As String = lblTanggal.Text
        Dim mm As String = lblTime.Text
        Dim spcLen14 As New String(yy + " " + mm)
        TextToPrint &= spcLen14 & Environment.NewLine

        Dim spcLen15 As New String(StrDup(62, "-"))
        TextToPrint &= spcLen15 & Environment.NewLine

        Dim StringToPrint As String = "Barang yang sudah dibeli"
        Dim LineLen As Integer = StringToPrint.Length
        Dim spcLen1 As New String(" "c, Math.Round((jumlahkarakter - LineLen) / 2))
        TextToPrint &= spcLen1 & StringToPrint & Environment.NewLine

        Dim StringToPrints As String = "tidak dapat di tukar / kembalikan"
        Dim LineLens As Integer = StringToPrint.Length
        Dim spcLen1s As New String(" "c, Math.Round((jumlahkarakter - LineLen) / 4))
        TextToPrint &= spcLen1s & StringToPrints & Environment.NewLine

    End Sub


    Sub get_nomor()
        konek_db()
        Dim tampil As New SqlCommand("select * from t_jual where id_transaksi IN(SELECT MAX(id_transaksi) from t_jual)", koneksi)

        reader = tampil.ExecuteReader
        reader.Read()
        If Not reader.HasRows Then
            txt_no_pesan.Text = Format(Now, "yyMMdd") + "0001"
        Else
            If Microsoft.VisualBasic.Left(reader("id_transaksi"), 4) Then
                txt_no_pesan.Text = reader("id_transaksi") + 1
            Else
                txt_no_pesan.Text = Format(Now, "yyMMdd") + "0001"
            End If
        End If
    End Sub

    Sub bersihteks()
        txt_barcode.Text = ""
        txt_bayar.Text = "0"
        txt_harga.Text = ""
        lblKDBarang.Text = ""
        lblHargaDiskon.Text = "harga"

        txt_nama.Text = ""
        txt_jml.Text = "1"
        lbl_total.Text = "0"
        kd_brng = ""
        txt_kembalian.Text = ""
        lblStok.Text = ""
        txtDiskon.Text = "0"
        'ecomerce
        txt_harga_ecomerce.Text = ""
        lblIDEcomerce.Text = ""
        lblJenisEcomerce.Text = ""
        lblStokEcomerce.Text = ""
        txt_jml_ecomerce.Text = ""
        txt_nomor.Text = ""
        lblBiayaEcomerce.Text = ""
        txt_harga_ecomerce.Text = Nothing
    End Sub

    Private Sub column()
        dgv.Columns.Add(0, "IDBarang")
        dgv.Columns.Add(1, "NamaBarang")
        dgv.Columns.Add(2, "Qty")
        dgv.Columns.Add(3, "Harga")
        dgv.Columns.Add(4, "SubTotal")
        dgv.Columns.Add(5, "ID")
        dgv.Columns.Add(6, "Diskon")
        dgv.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12)

        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgv.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        dgv.Columns(0).ReadOnly = True
        dgv.Columns(1).ReadOnly = True
        dgv.Columns(2).ReadOnly = True
        dgv.Columns(3).ReadOnly = True
        dgv.Columns(4).ReadOnly = True
        dgv.Columns(5).ReadOnly = True
        dgv.Columns(6).ReadOnly = True
    End Sub

    Private Sub columnEcomerce()
        dgvEcomerce.Columns.Add(0, "ID")
        dgvEcomerce.Columns.Add(1, "Jenis")
        dgvEcomerce.Columns.Add(2, "Nomor")
        dgvEcomerce.Columns.Add(3, "Harga")
        dgvEcomerce.Columns.Add(4, "SubTotal")
        dgvEcomerce.Columns.Add(5, "Biaya")
        dgvEcomerce.Columns.Add(6, "IDJenis")
        dgvEcomerce.Columns.Add(7, "Nomor")
        dgvEcomerce.DefaultCellStyle.Font = New Font("Microsoft Sans Serif", 12)

        dgvEcomerce.Columns(0).Width = "50"
        dgvEcomerce.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        dgvEcomerce.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvEcomerce.Columns(4).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvEcomerce.Columns(5).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        dgvEcomerce.Columns(0).ReadOnly = True
        dgvEcomerce.Columns(1).ReadOnly = True
        dgvEcomerce.Columns(2).ReadOnly = True
        dgvEcomerce.Columns(3).ReadOnly = True
        dgvEcomerce.Columns(4).ReadOnly = True
        dgvEcomerce.Columns(5).ReadOnly = True
        dgvEcomerce.Columns(6).ReadOnly = True
    End Sub

    Private Sub insertToDgv()
        Dim rowNum As Integer = dgv.Rows.Add()
        Dim price As Integer = txt_harga.Text
        Dim qty As Integer = txt_jml.Text
        Dim sub_Total As Integer = price * qty
        Dim diskon As Integer = txtDiskon.Text

        dgv.Rows(rowNum).Cells(0).Value = txt_barcode.Text
        dgv.Rows(rowNum).Cells(1).Value = txt_nama.Text
        dgv.Rows(rowNum).Cells(2).Value = txt_jml.Text
        dgv.Rows(rowNum).Cells(3).Value = txt_harga.Text
        dgv.Rows(rowNum).Cells(4).Value = sub_Total
        dgv.Rows(rowNum).Cells(5).Value = lblKDBarang.Text
        dgv.Rows(rowNum).Cells(6).Value = diskon
        Call bersihteks()
    End Sub

    Private Sub insertToDgvEcomerce()
        Dim rowNum As Integer = dgvEcomerce.Rows.Add()
        Dim price As Integer = txt_harga_ecomerce.Text
        Dim sub_Total As Integer = price

        dgvEcomerce.Rows(rowNum).Cells(0).Value = lblIDEcomerce.Text
        dgvEcomerce.Rows(rowNum).Cells(1).Value = lblJenisEcomerce.Text
        dgvEcomerce.Rows(rowNum).Cells(2).Value = txt_nomor.Text
        dgvEcomerce.Rows(rowNum).Cells(3).Value = txt_harga_ecomerce.Text
        dgvEcomerce.Rows(rowNum).Cells(4).Value = sub_Total
        dgvEcomerce.Rows(rowNum).Cells(5).Value = lblBiayaEcomerce.Text
        dgvEcomerce.Rows(rowNum).Cells(6).Value = lblJenisID.Text
        dgvEcomerce.Rows(rowNum).Cells(7).Value = txt_nomor.Text
        Call bersihteks()
    End Sub

    Private Sub sumTotal()
        Dim total As Integer = 0
        Dim diskon As Integer = 0
        Dim totalEcomerce As Integer = 0
        For i As Integer = 0 To dgv.RowCount - 2
            total += dgv.Rows(i).Cells(4).Value
            diskon += dgv.Rows(i).Cells(6).Value
        Next

        For i As Integer = 0 To dgvEcomerce.RowCount - 2
            totalEcomerce += dgvEcomerce.Rows(i).Cells(4).Value
        Next
        Dim alltotal As Integer = total + totalEcomerce
        lbl_total.Text = alltotal - diskon
        lblDiskon.Text = diskon
    End Sub

    Sub aksesteks(ByVal akses As Boolean)
        GroupBox1.Enabled = akses
        GroupBox2.Enabled = akses
    End Sub

    Private Sub headerOrder()
        Call konek_db()
        Dim sql As String
        Dim tanggal As String = lblTanggal.Text
        Dim waktu As String = lblTime.Text
        Dim total As Double = lbl_total.Text
        Dim jual As SqlCommand


        sql = "INSERT INTO t_jual VALUES(@id_transaksi, @uid, @tanggal, @waktu, @total, @bayar, @kembalian)"
        jual = New SqlCommand(sql, koneksi)
        jual.Parameters.AddWithValue("@id_transaksi", txt_no_pesan.Text)
        jual.Parameters.AddWithValue("@uid", mUID)
        jual.Parameters.AddWithValue("@tanggal", tanggal)
        jual.Parameters.AddWithValue("@waktu", waktu)
        jual.Parameters.AddWithValue("@total", total)
        jual.Parameters.AddWithValue("@bayar", txt_bayar.Text)
        jual.Parameters.AddWithValue("@kembalian", txt_kembalian.Text)
        jual.ExecuteNonQuery()

    End Sub

    Private Sub detailOrder()
        Call konek_db()
        Dim sql As String
        Dim detail As SqlCommand
        Dim ecomerce As SqlCommand
        Dim ecomerce_stok As SqlCommand
        Dim stok_u As SqlCommand

        For i As Integer = 0 To dgv.RowCount - 2
            Dim kd_barang As Integer = dgv.Item(5, i).Value
            Dim qty As Integer = dgv.Item(2, i).Value
            Dim harga As Integer = dgv.Item(3, i).Value
            Dim sub_total As Integer = harga * qty
            sql = "INSERT INTO t_detail_jual VALUES(@id_transaksi, @kd_barang, @qty, @harga_jual, @sub_total)"
            detail = New SqlCommand(sql, koneksi)
            detail.Parameters.AddWithValue("@id_transaksi", txt_no_pesan.Text)
            detail.Parameters.AddWithValue("@kd_barang", kd_barang)
            detail.Parameters.AddWithValue("@qty", qty)
            detail.Parameters.AddWithValue("@harga_jual", harga)
            detail.Parameters.AddWithValue("@sub_total", sub_total)
            detail.ExecuteNonQuery()

            Dim updateStok As String
            updateStok = "UPDATE t_barang set stok -= @stok where kd_barang=@kd_barang"
            stok_u = New SqlCommand(updateStok, koneksi)
            stok_u.Parameters.AddWithValue("stok", qty)
            stok_u.Parameters.AddWithValue("kd_barang", kd_barang)
            stok_u.ExecuteNonQuery()

        Next

        For i As Integer = 0 To dgvEcomerce.RowCount - 2
            Dim id_stok As Integer = dgvEcomerce.Item(0, i).Value
            Dim id_jenis As Integer = dgvEcomerce.Item(6, i).Value
            Dim nomor As String = dgvEcomerce.Item(2, i).Value
            Dim harga As Integer = dgvEcomerce.Item(3, i).Value
            Dim sub_total As Integer = harga

            sql = "INSERT INTO t_ecomerce VALUES(@id_transaksi,@id_stok, @nomor, @harga, @sub_total)"
            ecomerce = New SqlCommand(sql, koneksi)
            ecomerce.Parameters.AddWithValue("@id_transaksi", txt_no_pesan.Text)
            ecomerce.Parameters.AddWithValue("@id_stok", id_stok)
            ecomerce.Parameters.AddWithValue("@nomor", nomor)
            ecomerce.Parameters.AddWithValue("@harga", harga)
            ecomerce.Parameters.AddWithValue("@sub_total", sub_total)
            ecomerce.ExecuteNonQuery()

            Dim updateStokEcomerce As String
            Dim biaya As Double = dgvEcomerce.Item(5, i).Value
            Dim stokEcomerce As Double = harga + biaya
            updateStokEcomerce = "UPDATE t_ecomerce_jenis set stok -= @stok where id_jenis=@id_jenis"
            ecomerce_stok = New SqlCommand(updateStokEcomerce, koneksi)
            ecomerce_stok.Parameters.AddWithValue("stok", stokEcomerce)
            ecomerce_stok.Parameters.AddWithValue("id_jenis", id_jenis)
            ecomerce_stok.ExecuteNonQuery()
        Next

        MessageBox.Show("Insert Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Call bersihteks()
        dgv.Columns.Clear()
        dgvEcomerce.Columns.Clear()
        lbl_total.Text = ""
        Call sumTotal()
        Call columnEcomerce()
        Call column()
        txt_no_pesan.Text = ""
        GroupBox1.Enabled = True
        GroupBox3.Enabled = False
        txt_harga_ecomerce.Text = Nothing
        txt_barcode.Focus()
    End Sub

    Private Sub Form_transaksi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call get_nomor()
        txt_barcode.Focus()
        txtDiskon.Text = 0
        lblDiskon.Text = 0
        txt_jml_ecomerce.Enabled = False
        Call column()
        Call columnEcomerce()
        lblKDBarang.Text = ""
        txt_jml.Text = 1
        GroupBox1.Enabled = True
        lblTanggal.Text = System.DateTime.Now.ToString(("yyyy-MM-dd"))
        lblTime.Text = System.DateTime.Now.ToString(("HH:mm:ss"))
        txt_kembalian.Enabled = False 'POS58 10.0.0.6 mPrinter EPSON TM-220 Receipt
        PrintDocument1.PrinterSettings.PrinterName = mPrinter
        GroupBox3.Enabled = False
        lblBiayaEcomerce.Text = ""
        lblIDEcomerce.Text = ""
        lblJenisEcomerce.Text = ""
        lblStokEcomerce.Text = ""
        txt_bayar.Text = 0
        txt_kembalian.Text = 0
    End Sub

    Private Sub txt_proses_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            btn_tamabahkan.Focus()
            e.Handled = CBool(kata)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub btn_tamabahkan_Click_1(sender As Object, e As EventArgs) Handles btn_tamabahkan.Click
        If Val(txt_jml.Text) > Val(lblStok.Text) Then
            MessageBox.Show("Maaf Stok tidak mencukupi", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_jml.Focus()
        ElseIf txt_nama.Text = "" Or txt_jml.Text = "" Then
            MessageBox.Show("Tidak ada barang", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txt_barcode.Focus()
        Else

            Call insertToDgv()
            Call sumTotal()
            txt_barcode.Focus()
        End If
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        txt_jml.Text = Val(txt_jml.Text) + 1
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        If txt_jml.Text = "0" Then
            MessageBox.Show("Minus Tidak boleh", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            txt_jml.Text = Val(txt_jml.Text) - 1
        End If
    End Sub

    Private Sub btn_selesai_Click(sender As Object, e As EventArgs) Handles btn_selesai.Click
        If lbl_total.Text = "0" Then
            MessageBox.Show("Silahkan Pilih item", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If Val(txt_bayar.Text) < Val(lbl_total.Text) Then
                MessageBox.Show("Maaf Uang kurang", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                Dim dialog As DialogResult
                dialog = MessageBox.Show("Apakah akan di cetak?", "Dialog", MessageBoxButtons.YesNo)

                If dialog = Windows.Forms.DialogResult.No Then
                    Call headerOrder()
                    Call detailOrder()
                    Call get_nomor()
                Else
                    'Print()
                    Call PrintHeader()
                    Call ItemsToBePrinted()
                    Call ItemsEcomerce()
                    Call printFooter()
                    Dim printControl = New Printing.StandardPrintController
                    PrintDocument1.PrintController = printControl
                    Try
                        PrintDocument1.Print()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                    'Call print_struk()
                    Call headerOrder()
                    Call detailOrder()
                    Call get_nomor()
                End If
            End If
        End If
    End Sub

    Private Sub txt_barcode_TextChanged_1(sender As Object, e As EventArgs) Handles txt_barcode.TextChanged

    End Sub

    Private Sub txt_jml_TextChanged(sender As Object, e As EventArgs) Handles txt_jml.TextChanged

    End Sub

    Private Sub txt_bayar_TextChanged_1(sender As Object, e As EventArgs) Handles txt_bayar.TextChanged

        If txt_bayar.Text = "" Then
        Else
            txt_kembalian.Text = Val(txt_bayar.Text - lbl_total.Text)
        End If
    End Sub

    Private Sub dgv_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dgv.KeyPress
        On Error Resume Next
        If e.KeyChar = Chr(27) Then
            dgv.Rows().RemoveAt(dgv.CurrentCell.RowIndex)
            Call sumTotal()
            txt_bayar.Text = "0"
            txt_kembalian.Text = "0"
        End If
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txt_bayar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_bayar.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
            btn_selesai.Focus()
        End If

    End Sub

    Private Sub txt_barcode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_barcode.KeyPress
        If e.KeyChar = Chr(13) Then
            If txt_barcode.Text = "" Then
                MsgBox("Barang tidak ada")
            Else
                konek_db()
                Dim tampil As New SqlCommand("select * from t_barang where barcode = '" & txt_barcode.Text & "' or kd_barang = '" & txt_barcode.Text & "'", koneksi)
                reader = tampil.ExecuteReader
                Try
                    reader.Read()
                    txt_nama.Text = reader!nama_barang
                    txt_harga.Text = reader!harga_jual
                    lblKDBarang.Text = reader!kd_barang
                    lblStok.Text = reader!stok
                    lblHargaDiskon.Text = reader!harga_jual
                    txt_jml.Focus()
                    'Dim img() As Byte
                    'img = reader!foto
                    'Dim ms As New MemoryStream(img)
                    'pcBox.Image = Image.FromStream(ms)
                    'txt_jml.Focus()
                Catch ex As Exception
                    MsgBox("Gagal mengambil data barang karena , " + ex.Message, MsgBoxStyle.Critical)
                End Try
            End If

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Static currentChar As Integer
        'Untuk font struk direkomendasikan Font Telidon Hv
        Dim textfont As Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)

        Dim h, w As Integer
        Dim left, top As Integer
        With PrintDocument1.DefaultPageSettings
            h = 0
            w = 0
            left = 1
            top = 0
        End With

        Dim lines As Integer = CInt(Math.Round(h / 1))
        Dim b As New Rectangle(left, top, w, h)
        Dim format As StringFormat
        format = New StringFormat(StringFormatFlags.LineLimit)
        Dim line, chars As Integer

        e.Graphics.MeasureString(Mid(TextToPrint, currentChar + 1), textfont, New SizeF(w, h), format, chars, line)
        e.Graphics.DrawString(TextToPrint.Substring(currentChar, chars), New Font("Microsoft Sans Serif", 7, FontStyle.Regular), Brushes.Black, b, format)

        currentChar = currentChar + chars
        If currentChar < TextToPrint.Length Then
            e.HasMorePages = True
        Else
            e.HasMorePages = False
            currentChar = 0
        End If
    End Sub

    Private Sub txt_jml_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_jml.KeyPress
        If e.KeyChar = Chr(13) Then
            Dim kata As Short = Asc(e.KeyChar)
            If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
                kata = 0
            Else
                e.Handled = CBool(kata)
                If Val(txt_jml.Text) > Val(lblStok.Text) Then
                    MessageBox.Show("Maaf stok tidak ada", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txt_jml.Focus()
                Else
                    btn_tamabahkan.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form_jenis_ecomerce.Show()
    End Sub

    Private Sub Button4_Click_2(sender As Object, e As EventArgs) Handles Button4.Click
        Form_cari_barang_transaksi.Show()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If txt_no_pesan.Text = "" Then
            MessageBox.Show("Silahkan buat pesanan terlebih dahulu", "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf Not txt_nama.Text = "" Then
            MessageBox.Show("Silahkan tambahkan pesanan pertama terlebih dahulu", "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            GroupBox3.Enabled = True
            txt_harga_ecomerce.Enabled = False
            Form_jenis_ecomerce.Show()
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        GroupBox3.Enabled = False
        txt_harga_ecomerce.Text = ""
        txt_jml_ecomerce.Text = ""
        txt_nomor.Text = ""
        lblIDEcomerce.Text = ""
        lblStokEcomerce.Text = ""
    End Sub

    Private Sub txt_nomor_TextChanged(sender As Object, e As EventArgs) Handles txt_nomor.TextChanged

    End Sub

    Private Sub Form_transaksi_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If lblIDEcomerce.Text = "" Then
            MessageBox.Show("Tidak ada ecomerce, pilih item di button cari jenis", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Button5.Focus()
        ElseIf txt_nomor.Text = "" Or txt_jml_ecomerce.Text = "" Then
            MessageBox.Show("Tidak boleh ada yang kosong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            Call insertToDgvEcomerce()
            Call sumTotal()
            txt_harga_ecomerce.Text = ""
            txt_barcode.Focus()
        End If
    End Sub

    Private Sub txt_jml_ecomerce_SelectedIndexChanged(sender As Object, e As EventArgs)
        If txt_jml_ecomerce.Text = 5 Then
            txt_harga_ecomerce.Text = (5 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 10 Then
            txt_harga_ecomerce.Text = (10 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 15 Then
            txt_harga_ecomerce.Text = (15 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 20 Then
            txt_harga_ecomerce.Text = (20 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 25 Then
            txt_harga_ecomerce.Text = (25 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 50 Then
            txt_harga_ecomerce.Text = (50 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 60 Then
            txt_harga_ecomerce.Text = (60 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 100 Then
            txt_harga_ecomerce.Text = (100 * 1000) + 2000
        ElseIf txt_jml_ecomerce.Text = 200 Then
            txt_harga_ecomerce.Text = (200 * 1000) + 2000
        Else
            MsgBox("Tidak ada pilihan")
        End If
    End Sub

    Private Sub dgv_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub

    Private Sub dgvEcomerce_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvEcomerce.CellContentClick

    End Sub

    Private Sub txt_nomor_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txt_nomor.KeyPress
        If e.KeyChar = Chr(13) Then
            Button8.Focus()
        End If
    End Sub

    Private Sub txt_kembalian_TextChanged(sender As Object, e As EventArgs) Handles txt_kembalian.TextChanged

    End Sub

    Private Sub txt_harga_ecomerce_TextChanged(sender As Object, e As EventArgs) Handles txt_harga_ecomerce.TextChanged

    End Sub

    Private Sub txt_harga_TextChanged(sender As Object, e As EventArgs) Handles txt_harga.TextChanged

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles txtDiskon.TextChanged

    End Sub

    Private Sub dgvEcomerce_KeyPress(sender As Object, e As KeyPressEventArgs) Handles dgvEcomerce.KeyPress
        On Error Resume Next
        If e.KeyChar = Chr(27) Then
            dgvEcomerce.Rows().RemoveAt(dgvEcomerce.CurrentCell.RowIndex)
            Call sumTotal()
            txt_bayar.Text = "0"
            txt_kembalian.Text = "0"
        End If
    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub txt_jml_ecomerce_TextChanged(sender As Object, e As EventArgs) Handles txt_jml_ecomerce.TextChanged

    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiskon.KeyPress
        Dim kata As Short = Asc(e.KeyChar)
        If (e.KeyChar Like "[0-9]" OrElse kata = Keys.Back) Then
            kata = 0
        Else
            e.Handled = CBool(kata)
            btn_tamabahkan.Focus()
        End If
    End Sub
End Class