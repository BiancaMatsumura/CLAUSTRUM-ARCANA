using System.Collections.Generic;
using UnityEngine;


public class Passwords: MonoBehaviour
{
    // Cada senha é representada como uma string para facilitar o input no Inspector
    [SerializeField]
    private List<string> passwordStrings = new List<string>();

    private List<List<int>> passwords = new List<List<int>>();
    private int currentPasswordIndex = 0;

    private void Start()
    {
        // Converta as strings do Inspector para listas de inteiros
        foreach (string passwordString in passwordStrings)
        {
            List<int> password = new List<int>();
            foreach (char c in passwordString)
            {
                if (int.TryParse(c.ToString(), out int number))
                {
                    password.Add(number);
                }
            }
            passwords.Add(password);
        }
    }

    public List<int> GetCurrentPassword()
    {
        if (currentPasswordIndex < passwords.Count)
        {
            return passwords[currentPasswordIndex];
        }
        return null;
    }

    public bool AdvanceToNextPassword()
    {
        if (currentPasswordIndex < passwords.Count - 1)
        {
            currentPasswordIndex++;
            return true;
        }
        return false; // Todas as senhas foram usadas
    }

    public int GetCurrentPasswordIndex()
    {
        return currentPasswordIndex;
    }

    public bool HasMorePasswords()
    {
        return currentPasswordIndex < passwords.Count;
    }



}

