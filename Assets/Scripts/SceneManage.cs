using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Tambahkan ini

public class SceneManage : MonoBehaviour
{
    // Fungsi ini akan dipanggil dari Animation Event atau tombol
    public void GantiScene()
    {
        SceneManager.LoadScene("NamaScene"); // Ganti dengan nama scene kamu
    }
}
