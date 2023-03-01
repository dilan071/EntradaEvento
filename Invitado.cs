using System;
using System.Collections.Generic;
using System.IO;

// Clase que representa un invitado al evento
public class Invitado
{
    public string Nombre { get; set; } // Nombre del invitado
    public int Id { get; set; } // Identificador del invitado
    public string Email { get; set; } // Correo electrónico del invitado
    public int Edad { get; set; } // Edad del invitado
}

// Clase que representa una lista de invitados cargada desde un archivo de texto
public class ListaInvitadosTxt : List<Invitado>
{
    public ListaInvitadosTxt(string archivo)
    {
        // Abrir el archivo para lectura
        using (StreamReader reader = new StreamReader(archivo))
        {
            string line;
            // Leer cada línea del archivo
            while ((line = reader.ReadLine()) != null)
            {
                // Separar los datos de la línea por coma
                string[] datos = line.Split(',');
                // Extraer los datos del invitado de cada posición del arreglo
                string nombre = datos[0];
                int id = int.Parse(datos[1]);
                string email = datos[2];
                int edad = int.Parse(datos[3]);
                // Crear un objeto Invitado con los datos extraídos y agregarlo a la lista
                Invitado invitado = new Invitado { Nombre = nombre, Id = id, Email = email, Edad = edad };
                Add(invitado);
            }
        }
    }
}

// Clase que representa una lista de invitados cargada desde un archivo CSV
public class ListaInvitadosCsv : List<Invitado>
{
    public ListaInvitadosCsv(string archivo)
    {
        // Abrir el archivo para lectura
        using (StreamReader reader = new StreamReader(archivo))
        {
            string line;
            // Leer cada línea del archivo
            while ((line = reader.ReadLine()) != null)
            {
                // Separar los datos de la línea por coma
                string[] datos = line.Split(',');
                // Extraer los datos del invitado de cada posición del arreglo
                string nombre = datos[0];
                int id = int.Parse(datos[1]);
                string email = datos[2];
                int edad = int.Parse(datos[3]);
                // Crear un objeto Invitado con los datos extraídos y agregarlo a la lista
                Invitado invitado = new Invitado { Nombre = nombre, Id = id, Email = email, Edad = edad };
                Add(invitado);
            }
        }
    }
}

// Clase que valida los datos de un invitado
public class ValidadorInvitado
{
    private string[] dominiosValidos = { "gmail", "hotmail", "live" };

    // Método que valida el formato y dominio de un correo electrónico
    public bool ValidarEmail(string email)
    {
        string regexEmail = @"^[a-zA-Z][a-zA-Z0-9._-]*@[a-zA-Z]+(\.[a-zA-Z]+)+$";
        if (!System.Text.RegularExpressions.Regex.IsMatch(email, regexEmail))
            return false;
        string[] partesEmail = email.Split('@');
        string dominio = partesEmail[1].Split('.')[0];
        if (Array.IndexOf(dominiosValidos, dominio) < 0)
            return false;
        return true;
    }
    public bool ValidarEdad(int edad)//El método realiza una verificación simple,
    {                                // comprobando si la edad es mayor o igual a 18,
        if (edad >= 18)              // y devuelve true si es así y false en caso contrario.
            return true;
        return false;
    }

    public bool ValidarInvitado(Invitado invitado)
    {
        // Comprueba si tanto el email como la edad del invitado son válidos
        if (ValidarEmail(invitado.Email) && ValidarEdad(invitado.Edad))
            return true;  // Devuelve verdadero si ambos son válidos
        return false;  // Devuelve falso si uno o ambos son inválidos
    }

    public class ControladorEvento
    {
        private string archivo;
        private List<Invitado> listaInvitados;
        private ValidadorInvitado validadorInvitado;

        public ControladorEvento(string archivo)
        {
            this.archivo = archivo;
            // Determina qué tipo de archivo se está procesando y crea una lista de invitados correspondiente
            if (archivo.EndsWith(".txt"))
                listaInvitados = new ListaInvitadosTxt(archivo);
            else if (archivo.EndsWith(".csv"))
                listaInvitados = new ListaInvitadosCsv(archivo);
            // Crea una instancia del validador de invitados
            validadorInvitado = new ValidadorInvitado();
        }

        public void IniciarEvento()
        {
            // Itera a través de la lista de invitados y determina quién puede ingresar al evento
            foreach (Invitado invitado in listaInvitados)
            {
                if (validadorInvitado.ValidarInvitado(invitado))
                    Console.WriteLine(invitado.Nombre + " puede ingresar");
                else
                    Console.WriteLine(invitado.Nombre + " no puede ingresar");
            }
        }
    }
}