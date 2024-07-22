Imports System.Data.OleDb

Public Class NouveauBR

    ' Method to get the database connection
    Private Function GetConnection() As OleDbConnection
        ' Replace with your actual database file path
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\asus\Documents\WindowsApp4\WindowsApp4\Projet2.accdb"
        Return New OleDbConnection(connectionString)
    End Function

    Private Sub NouveauBR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateComboBox(ComboBox1, "SELECT Ferme FROM Ferme", "Ferme")
        PopulateComboBox(ComboBox2, "SELECT Varietes FROM Varietes", "Varietes")
        PopulateComboBox(ComboBox3, "SELECT Matricule FROM Chauffeur", "Matricule")
    End Sub

    ' Method to populate a ComboBox
    Private Sub PopulateComboBox(comboBox As ComboBox, selectQuery As String, columnName As String)
        Using connection As OleDbConnection = GetConnection()
            Using command As New OleDbCommand(selectQuery, connection)
                Try
                    connection.Open()
                    Using reader As OleDbDataReader = command.ExecuteReader()
                        While reader.Read()
                            comboBox.Items.Add(reader(columnName).ToString())
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Method to insert data into the database
    Private Sub InsertData(insertQuery As String, parameters As OleDbParameter())
        Using connection As OleDbConnection = GetConnection()
            Using command As New OleDbCommand(insertQuery, connection)
                command.Parameters.AddRange(parameters)

                Try
                    connection.Open()
                    Dim result As Integer = command.ExecuteNonQuery()

                    ' Check if the insert was successful
                    If result > 0 Then
                        MessageBox.Show("Data inserted successfully.")
                    Else
                        MessageBox.Show("Data insert failed.")
                    End If
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Event handler for the Insert button
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Check if required fields are filled and Poids, Cout, NombresdeCaisses are numeric
        If ComboBox1.SelectedIndex = -1 Then
            Label15.Text = "Please fill Ferme"
        ElseIf ComboBox2.SelectedIndex = -1 Then
            Label15.Text = "Please fill Varietes"
        ElseIf ComboBox3.SelectedIndex = -1 Then
            Label15.Text = "Please fill Chauffeur"
        ElseIf Not IsNumeric(Poids.Text) Then
            Label15.Text = "Poids must be a numeric value"
        ElseIf Not IsNumeric(Cout.Text) Then
            Label15.Text = "Cout must be a numeric value"
        ElseIf Not IsNumeric(NombresdeCaisses.Text) Then
            Label15.Text = "Nombres de Caisses must be a numeric value"
        Else
            ' All required fields are filled and numeric, insert data
            Dim insertQuery As String = "INSERT INTO Reception (NumeroBR, Varietes, Ferme, Chauffeur, Poids, Cout, NombresdeCaisses) " & "VALUES (?, ?, ?, ?, ?, ?, ?)"

            ' Create parameters
            Dim parameters As OleDbParameter() = {
                New OleDbParameter("@NumeroBR", TextBox2.Text),
                New OleDbParameter("@Varietes", ComboBox2.SelectedItem.ToString()),
                New OleDbParameter("@Ferme", ComboBox1.SelectedItem.ToString()),
                New OleDbParameter("@Chauffeur", ComboBox3.SelectedItem.ToString()),
                New OleDbParameter("@Poids", Convert.ToDouble(Poids.Text)),
                New OleDbParameter("@Cout", Convert.ToDouble(Cout.Text)),
                New OleDbParameter("@NombresdeCaisses", Convert.ToDouble(NombresdeCaisses.Text))
            }

            ' Call the insert method
            InsertData(insertQuery, parameters)
        End If
    End Sub

End Class
