Imports System.Data.SqlClient
Module ModKoneksi
    Public koneksi As SqlConnection = Nothing
    Public Sub konek_db()
        Try
            Dim server As String
            'DESKTOP-J9G4PAQ
            server = "server=HOYIRUL\HOYIRULSQL;database=db_project;integrated security= true"
            koneksi = New SqlConnection(server)
            koneksi.Open()
        Catch ex As Exception
            MsgBox("Koneksi Gagal", MsgBoxStyle.Critical)
        End Try
    End Sub
End Module
