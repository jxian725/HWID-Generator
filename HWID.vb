Imports System.Management
Imports System.Security.Cryptography
Imports System.Text


Public Class HWID
    Private Sub HWID_Load(sender As Object, e As EventArgs) Handles Me.Load
        CheckBox1.Checked = True
        CheckBox2.Checked = True
        CheckBox3.Checked = True
        CheckBox4.Checked = True
    End Sub

    Dim condition As Integer
    Private Sub ButtonGenerate_Click(sender As Object, e As EventArgs) Handles ButtonGenerate.Click
        If CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            condition = 1000
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            condition = 100
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then
            condition = 10
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then
            condition = 1
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            condition = 1100
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then
            condition = 1110
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            condition = 1111
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = False Then
            condition = 110
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            condition = 111
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            condition = 11
        ElseIf CheckBox1.Checked = False And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then
            condition = 101
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = False Then
            condition = 1010
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = True Then
            condition = 1001
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = True And CheckBox3.Checked = False And CheckBox4.Checked = True Then
            condition = 1101
        ElseIf CheckBox1.Checked = True And CheckBox2.Checked = False And CheckBox3.Checked = True And CheckBox4.Checked = True Then
            condition = 1011
        End If
        TextBox1.Text = Get_HWID()
        'MsgBox("Your HWID is = " & Get_HWID())
    End Sub



    Public Function Get_HWID() As String
        Dim strMotherBoardID As String = Nothing
        Dim query As New SelectQuery("Win32_BaseBoard")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strMotherBoardID = info("SerialNumber").ToString()
        Next
        Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", "C"))
        disk.Get()
        Dim mc As ManagementClass = New ManagementClass("Win32_NetworkAdapterConfiguration")
        Dim moc As ManagementObjectCollection = mc.GetInstances()
        Dim MACAddress As String = String.Empty
        For Each mo As ManagementObject In moc
            If (MACAddress.Equals(String.Empty)) Then
                If CBool(mo("IPEnabled")) Then MACAddress = mo("MacAddress").ToString()
                mo.Dispose()
            End If
            MACAddress = MACAddress.Replace(":", String.Empty)
        Next
        Dim strProcessorId As String = String.Empty
        Dim GHHKJGK As New SelectQuery("Win32_processor")
        Dim YUGYUKJH As New ManagementObjectSearcher(GHHKJGK)
        Dim fghfgh As ManagementObject
        For Each fghfgh In YUGYUKJH.Get()
            strProcessorId = fghfgh("processorId").ToString()
        Next
        Dim sha512 As Security.Cryptography.SHA512 = Security.Cryptography.SHA512Managed.Create()
        Dim bytes As Byte()
        If condition = 1111 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & disk("VolumeSerialNumber").ToString() & strMotherBoardID & MACAddress)
        ElseIf condition = 1 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(MACAddress)
        ElseIf condition = 11 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strMotherBoardID & MACAddress)
        ElseIf condition = 111 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(disk("VolumeSerialNumber").ToString() & strMotherBoardID & MACAddress)
        ElseIf condition = 1000 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId)
        ElseIf condition = 100 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(disk("VolumeSerialNumber").ToString())
        ElseIf condition = 10 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strMotherBoardID)
        ElseIf condition = 1001 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & MACAddress)
        ElseIf condition = 110 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(disk("VolumeSerialNumber").ToString() & strMotherBoardID)
        ElseIf condition = 1010 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & strMotherBoardID)
        ElseIf condition = 101 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(disk("VolumeSerialNumber").ToString() & MACAddress)
        ElseIf condition = 1110 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & disk("VolumeSerialNumber").ToString() & strMotherBoardID)
        ElseIf condition = 1100 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & disk("VolumeSerialNumber").ToString())
        ElseIf condition = 1011 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & strMotherBoardID & MACAddress)
        ElseIf condition = 1101 Then
            bytes = System.Text.Encoding.UTF8.GetBytes(strProcessorId & disk("VolumeSerialNumber").ToString() & MACAddress)
        End If
        Dim hash As Byte() = sha512.ComputeHash(bytes)
        Dim stringBuilder As New System.Text.StringBuilder()
        For i As Integer = 0 To hash.Length - 1
            stringBuilder.Append(hash(i).ToString("X2"))
        Next
        Return stringBuilder.ToString()
    End Function

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            ButtonGenerate.Enabled = False
        Else
            ButtonGenerate.Enabled = True
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            ButtonGenerate.Enabled = False
        Else
            ButtonGenerate.Enabled = True
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            ButtonGenerate.Enabled = False
        Else
            ButtonGenerate.Enabled = True
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox1.Checked = False And CheckBox2.Checked = False And CheckBox3.Checked = False And CheckBox4.Checked = False Then
            ButtonGenerate.Enabled = False
        Else
            ButtonGenerate.Enabled = True
        End If
    End Sub
End Class
