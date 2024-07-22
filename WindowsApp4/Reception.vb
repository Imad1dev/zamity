Imports System.Data.OleDb
Imports System.Drawing

Public Class Reception

    Private dataGridView1 As DataGridView
    Private bindingSource1 As BindingSource

    ' Method to get the database connection
    Private Function GetConnection() As OleDbConnection
        ' Replace with your actual database file path
        Dim connectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\asus\Documents\WindowsApp4\WindowsApp4\Projet2.accdb"
        Return New OleDbConnection(connectionString)
    End Function

    ' Method to load data from the Reception table and bind it to the DataGridView
    Private Sub LoadReceptionData()
        Dim query As String = "SELECT NumeroBR, Varietes, Ferme, Chauffeur, Poids, Cout, NombresdeCaisses FROM Reception"

        Using connection As OleDbConnection = GetConnection()
            Using adapter As New OleDbDataAdapter(query, connection)
                Dim table As New DataTable()

                Try
                    connection.Open()
                    adapter.Fill(table)
                    bindingSource1.DataSource = table
                    dataGridView1.DataSource = bindingSource1
                Catch ex As Exception
                    MessageBox.Show("Error: " & ex.Message)
                End Try
            End Using
        End Using
    End Sub

    ' Method to configure the DataGridView with a flat design
    Private Sub ConfigureDataGridView()
        dataGridView1 = New DataGridView()
        bindingSource1 = New BindingSource()

        ' Set the size and location
        dataGridView1.Size = New Size(920, 300)
        dataGridView1.Location = New Point(20, 130)
        dataGridView1.BackgroundColor = Color.FromArgb(240, 240, 240)

        ' Flat design settings
        dataGridView1.BorderStyle = BorderStyle.None
        dataGridView1.EnableHeadersVisualStyles = False
        dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None

        ' Column header style 
        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
        dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black
        dataGridView1.ColumnHeadersDefaultCellStyle.Font = New Font("Segoe UI", 10, FontStyle.Bold)
        dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dataGridView1.ColumnHeadersHeight = 50

        ' Cell style
        dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 210)
        dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black
        dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(250, 250, 250)
        dataGridView1.DefaultCellStyle.ForeColor = Color.Black
        dataGridView1.DefaultCellStyle.Font = New Font("Segoe UI", 9, FontStyle.Regular)
        dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft
        dataGridView1.RowTemplate.Height = 30 ' Set the cell height to 50

        ' Grid color
        dataGridView1.GridColor = Color.FromArgb(230, 230, 230)

        ' Set columns to fill the width of the DataGridView
        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        ' Alternating row colors
        dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245)

        ' Remove row headers
        dataGridView1.RowHeadersVisible = False

        ' Add the DataGridView to the form
        Me.Controls.Add(dataGridView1)
    End Sub

    ' Event handler for the Load event of the form
    Private Sub Reception_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigureDataGridView()
        LoadReceptionData()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        NouveauBR.Show()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0 ' First item selected
                Chauffeur.Show()

            Case 1 ' First item selected
                Ferme.Show()

            Case 2 ' First item selected
                Varietes.Show()

            Case Else
                ' Handle default case or do nothing if not needed
        End Select
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim filter As String = String.Format("NumeroBR LIKE '%{0}%'", TextBox1.Text)
        bindingSource1.Filter = filter
    End Sub
End Class
