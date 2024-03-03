Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1
    Const KateFilePath As String = "C:\Users\Klein\Documents\mk\Book Publishing\KateTmp.txt"
    Const KateFilePath1 As String = "C:\Users\Klein\Documents\mk\Book Publishing\KateTmpGit.txt"
    Const MattFilePath As String = "C:\Users\Klein\Documents\mk\Book Publishing\MattTmp.txt"
    Const MattFilePath1 As String = "C:\Users\Klein\Documents\mk\Book Publishing\MattTmpGit.txt"

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        system.IO.File.Delete(MattFilePath1)
        system.IO.File.Delete(KateFilePath1)
        ConvertFile(MattFilePath, MattFilePath1)
        ConvertFile(KateFilePath, KateFilePath1)
    End Sub

    Private Sub ConvertFile(filenamein As String, filenameOut As String)

        Dim saetzeTMp As New List(Of String)
        ' Read all lines from the file into an array
        Dim lines As String() = File.ReadAllLines(filenamein)
        For Each line As String In lines
            dim orgline = line
            line = line.Replace(".""", "§""")
            line = line.Replace("?""", "§§""") '”
            ' Define the regular expression pattern
            Dim pattern As String = "([^.!?]+[.!?])"

            ' Match the pattern in the input string
            Dim matches As MatchCollection = Regex.Matches(line, pattern)
            dim saetze() as String = Regex.Split(line, pattern)
            If saetze.Count = 0 Then
                saetzeTMp.Add(orgline)
            Else
                For Each lineTmp As String in saetze
                    lineTmp = lineTmp.Replace("§§""", "?""")
                    lineTmp = lineTmp.Replace("§""", ".""").Trim()
                    lineTmp = lineTmp.Trim()
                    if not String.IsNullOrWhiteSpace(lineTmp) Then
                        saetzeTMp.Add(lineTmp)
                    End If
                Next
            End If

        Next

        File.WriteAllLines(filenameOut, saetzeTMp)
        TextBox1.Text = "Saetze Anzahl: " + saetzeTMp.Count.ToString() + ControlChars.CrLf + ControlChars.CrLf + String.Join(ControlChars.CrLf, saetzeTMp)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button1.Text = "Process Files"
    End Sub
End Class
