﻿Imports System.Data.OleDb
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class Form_Lap_Barang

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim cryrpt As New ReportDocument
            Dim crtablelogoninfos As New TableLogOnInfos
            Dim crtablelogoninfo As New TableLogOnInfo
            Dim crconnectioninfo As New ConnectionInfo
            Dim crtables As Tables
            Dim crtable As Table
            Dim laporan As New Lap_barang
            cryrpt = laporan
            With crconnectioninfo
                .ServerName = Application.StartupPath & "\db_penjualan.mdb"
                .DatabaseName = Application.StartupPath & "\db_penjualan.mdb"
                .UserID = ""
                .Password = ""
            End With
            crtables = cryrpt.Database.Tables
            For Each crtable In crtables
                crtablelogoninfo = crtable.LogOnInfo
                crtablelogoninfo.ConnectionInfo = crconnectioninfo
                crtable.ApplyLogOnInfo(crtablelogoninfo)
            Next
            CrystalReportViewer1.ReportSource = cryrpt
            CrystalReportViewer1.Refresh()
            CrystalReportViewer1.RefreshReport()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CrystalReportViewer1.PrintReport()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CrystalReportViewer1.ExportReport()
    End Sub
End Class