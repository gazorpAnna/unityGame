using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LoginConectionBD : MonoBehaviour {

    //Login
	public bool isAnna;
	public string		mapa = "main";
	public InputField	textUser;
	public InputField	textPassword;
    public string nameUser;
    private bool isUser = false;
    public string surnameUser;

    //register
    public InputField userText;
    public InputField nameText;
    public InputField surnameText;
    public InputField emailText;
    public InputField passwordText;
    // Códigos

    //400 --> No hay conexión con la base de datos
    //401 --> Usuario y/o contraseña incorrectos (no se encuentra en la BD)
    //402 --> No se puede registrar usuario. Este Usuario ya existe en la BD;

    //200 --> Usuario encontrado!
    //201 --> Usuario registrado correctamente !
	public void iniciarSesion()
	{
		StartCoroutine (login ());
	}
	public void goRegister()
	{
		SceneManager.LoadScene ("registerScene");
	}

    public void registrarse()
    {
        StartCoroutine(register());
    }

    IEnumerator login()
    {
		print ("Loginn");
		WWW conect;
		if (isAnna) 
		{
			print ("Hola Anna");
			conect = new WWW ("192.168.58//game_1o_page/login.php?uss=" + textUser.text + "&pss=" + textPassword.text);
		} 
		else 
		{
			print ("Hola Joan");
			conect = new WWW ("http://localhost/game_1o_page/login.php?uss=" + textUser.text + "&pss=" + textPassword.text);
		}
        yield return (conect);
        switch (conect.text)
        {
            case "401":
                print("Usuario y/o contraseña incorrectos ");
                break;
            case "400":
                print("No se ha podido establecer conexión con la base datos");
                break;

            case "200":
                print("Usuario conectado correctamente !");
				//SceneManager.LoadScene(mapa);
                datos();
                break;
        }
       
    }

    IEnumerator datos()
    {
        WWW conect = new WWW("http://localhost/game_1o_page/datos.php?uss=" + textUser.text);
        yield return (conect);
        switch (conect.text)
        {
            case "401":
                print("Usuario y/o contraseña incorrectos ");
                break;
            case "400":
                print("No se ha podido establecer conexión con la base datos");
                break;

            default:
                string[] datos = conect.text.Split('|');
                if (datos.Length != 2)
                {
                    print("Error en la conexión");
                }
                else
                {
                    nameUser = datos[0];
                    surnameUser = datos[1];
					
                }

                break;
        }
    }
        IEnumerator register()
        {
            WWW conect = new WWW("http://localhost/game_1o_page/register.php?uss=" + userText.text+"&name="+nameText.text+"&sname="+surnameText.text+"&email="+emailText.text+"&pss="+passwordText.text);
            yield return (conect);
            switch (conect.text)
            {
                case "402":
                    print("Usuario ya existe ");
                    break;
                case "400":
                    print("No se ha podido establecer conexión con la base datos");
                    break;

                case "201":
                    print("Usuario creado correctamente!");
                    break;
            }

        }
    }
