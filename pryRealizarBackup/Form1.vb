Imports System.Data.SqlClient

Public Class Form1

    Dim conex As New SqlConnection("DATA SOURCE = EQUIPO\MSSQL2; INITIAL CATALOG = dbGestionVentas; INTEGRATED SECURITY = true")
    Dim cmd As SqlCommand
    Dim fecha As String = Format(DateTime.Now, "_dd_MM_yyy_h-mm")
    Dim archivo As String = "dbGestionVentas" & fecha & ".bak"
    Dim ubicacion As String = "C:\Users\Usuario1\Documents\MEGA\"
    Dim parametro As String

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        llenarText()
    End Sub

    Sub llenarText()
        Me.txtNombre.Text = archivo
        Me.txtDirectorio.Text = ubicacion
    End Sub

    Sub crearBackup()
        conex.Close()
        Try
            parametro = ubicacion & archivo
            cmd = New SqlCommand
            With cmd
                .Connection = conex
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_CrearBackup"
                .Parameters.AddWithValue("@Ubicacion", parametro)
            End With

            conex.Open()
            cmd.ExecuteNonQuery()
            conex.Close()
            MsgBox("Backup creado correctamente!", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Operación exitosa!")
        Catch ex As Exception
            MsgBox("Error al crear Backup" & Chr(13) & ex.Message, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Error!")
        End Try
    End Sub

    Private Sub btnOperacion_Click(sender As Object, e As EventArgs) Handles btnOperacion.Click
        crearBackup()
    End Sub

End Class
