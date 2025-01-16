using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class AuthLogin : MonoBehaviour
{
    public DependencyStatus dependencyStatus;
    private FirebaseAuth auth;
    public FirebaseUser user;

    [Header("Login")]
    [SerializeField] private TMP_InputField emailLoginField;
    [SerializeField] private TMP_InputField passwordLoginField;
    [SerializeField] private TMP_Text warningLoginText;
    public UnityEvent OnLoginSuccesful;

    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
            }
            else
            {
                Debug.Log("could not resolve firebase dependency" + dependencyStatus);
            }
        });
    }


    public void LoginButton()
    {
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    private IEnumerator Login(string _email, string _password)
    {

        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);

        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);


        if (LoginTask.Exception != null)
        {
            Debug.Log(message: $"Failed To register task with{LoginTask.Exception}");
            FirebaseException firebaseEX = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEX.ErrorCode;

            string message = "Login Failed: " + errorCode;
            warningLoginText.text = message;
        }
        else
        {
            Firebase.Auth.AuthResult result = LoginTask.Result;

            user = result.User;
            Debug.LogFormat("Firebase user sign-in successfully: {0} ({1})", user.DisplayName, user.Email);
            OnLoginSuccesful?.Invoke();
        }
    }
}
