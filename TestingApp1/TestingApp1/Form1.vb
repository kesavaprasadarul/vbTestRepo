﻿Imports System.IO.Ports

Public Class Form1

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not SerialPort1.IsOpen Then

            If TextBox1.Text = String.Empty Or TextBox2.Text = String.Empty Then
                MsgBox("Please fill all fields", MsgBoxStyle.Critical, "Incomplete")
                Return
            End If

            SerialPort1.PortName = TextBox1.Text
            SerialPort1.BaudRate = TextBox2.Text
            SerialPort1.Parity = ComboBox1.SelectedIndex + 1

            Try
                SerialPort1.Open()
                MsgBox("Port successfully opened", MsgBoxStyle.Information, "Serial Port Opened")
                GroupBox1.Enabled = True
                GroupBox2.Enabled = True
                Button1.Text = "Close Port"
            Catch ex As Exception
                MsgBox("Error opening port: " + ex.Message, MsgBoxStyle.Critical, "Serial Port Not Opened")
            End Try

        Else
            SerialPort1.Close()
            GroupBox1.Enabled = False
            GroupBox2.Enabled = False
            Button1.Text = "Open Port"
        End If

    End Sub

    Private Sub TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox2.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        'TODO: MAKE AS EXPECTED, THINGS WORK FINE
        Dim ch = SerialPort1.ReadExisting()
        If ch.EndsWith("?") Then
            ch = ch.Remove(ch.Length - 1)
        End If
        If ch = ChrW(127) Then
            Try
                RXText.Text = RXText.Text.Remove(RXText.Text.Length - 1)
            Catch ex As Exception

            End Try
            Return
        End If
        RXText.Text = RXText.Text + ch
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim toSend = TextBox4.Text
        SerialPort1.WriteLine(toSend)
    End Sub

End Class
