using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;


public class TankerScript : NetworkBehaviour
{
    ///             Server kurulduğunda,server kendisine bağlanan her bir oyuncu için bir client oluşturur.Bu client içerisinde 
    ///  kendisine bağlanan oyuncuyu temsil eden bir gameobjesi mevcuttur.Aynı zamanda bu clientın içerisinde diğer oyuncuları da temsil eden 
    ///  gameobjeleri de vardır(Eğer olmaz ise local oyuncu diğer oyuncuları göremez).Server, client içerisinde,kendisine bağlanan oyuncu için
    ///  bir local gameobjesi atar.Diğer gameobjeleri ise non-local  gameobjesi olarak işaretler.
    ///  
    ///            
    ///  

    ///           Host 
    ///           
    ///          Bir clientın aynı zamanda server kurup oyuncuların bağlanabilmesine olanak sağlayan server tipi.Üzerinde oyuncu bulundurur.
    ///  Client aynı zamanda bir oyuncudur ve oyuncu kendi kurduğu servera bağlanıp oyunu oynayabilir fakat internet bağlantısı kötü ise diğer 
    ///  clientlarda bundan etkilenir ve oyunda yavaşlamalara sebebiyet verir.
    ///  


    ///          Dedicated server 
    ///          
    ///          Sadece oyuncuların bağlanması ve oyunu oynayabilmeleri için kurulmuş server tipi.Üzerinde gerçek oyuncu bulundurmaz 
    /// 


    ///          [Command]   
    ///
    ///          Clientta çağırılır.Serverda çalışır.Eğer kullanılmaz ise host ve diğer clientlar ,herhangi bir clienttin tetiklediği metotları göremez
    /// Örnek Küp mermisi.
    /// Client,host tarafından kendisine fırlatılan küp mermisini görür.Kendi attığı küp mermisini de görür.Host kendi fırlattığı küp mermisini görür
    /// fakat clientlar tarafından kendisine fırlatılan küp mermilerini göremez.Clientlar birbirlerinin attıkları mermileri de göremezler.Bir nevi
    /// local bir fırlatma gerçekleşir.Fırlatma fonksiyonu client icindeki objede tetiklendiğinden ,server bunu algılayamaz çünkü client icindeki
    /// Player 1 local objesinin içindeki scriptten çalıştırılır ve serverdaki Player 1 objesinin bundan haberi yoktur.Eğer [command] kullanırsak aslında 
    /// client içindeki Player 1 local objesinin içindeki metot değil de ,serverdaki Player 1 objesinin içindeki metot çalıştırılır.
    /// 


    ///          [ClientRPC] 
    ///          
    ///          Serverda çağırılır.Tüm Clientlarda çalışır
    ///          

    ///          [SyncVar]    
    ///          
    ///          Oyuncuya ait local değerlerin clientlar arasında senkronize olması için gereklidir.Örnek:Player Health
    ///                   

    ///          isLocalPlayer 
    ///          
    ///          Normalde oyuncu non-local gameobjelerine erişebilir.İçerisindeki koda müdahale edebilir.Yani toplamda 3 oyuncu var ise bu oyuncu
    ///  diğer 2 karakteride hareket ettirebilir(eğer hareket komutu var ise).Bunun önüne geçebilmek için script içerisinde bu boolu kullanmak gerekir.
    ///  Eğer clientın içindeki local gameobjesi o obje ise true döndürür.Non-local gameobjeleri ise false döndürür.Böylelikle local oyuncunun 
    ///  diğer clientlara ait karakterlere erişmesinin ve hareket ettirmesinin(diğer oyunculara müdahale) önüne geçmis oluruz.
    ///  Her bir client için bir local gameobjesi mevcut olmak zorundadır.Aksi takdirde oyuna bağlanan oyuncu oyunu oynayamaz.
    ///  Bir client içerisindeki toplam non-local gameobject sayısı ,toplam oyuncu sayısına eşittir.Her bir oyuncuyu görebilmek ve etkileşime girebilmek
    ///  için bu şarttır.Oyuncunun kontrol etmesi gereken kısımlar için bu boolu kullanmak gerekir.Hareket etmek,mermi atmak gibi..
    ///  

    ///          isServer  
    ///          
    ///          Bu komut ,server içerisindeki tüm objelerde çalışır ve yalnızca serverda çalışmasını istediğimiz komutların burada çalışmasını 
    ///  sağlamak için bunu kullanırız.
    ///  
    ///  
    ///  

    public TMP_Text HealthText;
    public Rigidbody ShipRigid;
    public float ShipSpeed;
    public float shipRotationSpeed;
    public float CamSpeed;
    [SyncVar] public int Health;
    public GameObject bullet;
    public Transform bulletPos;


    // Start is called before the first frame update
    void Start()
    {
        if (!isLocalPlayer)
            return;

        ShipRigid = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        float vertical = Input.GetAxis("Vertical");

        ShipRigid.MovePosition(ShipRigid.position + transform.forward* Time.fixedDeltaTime * ShipSpeed * vertical);


    }

    public void Rotate() {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * -horizontal * shipRotationSpeed);
    
    
    }

    private void CamMovement()
    {
        Vector3 CamPos = Camera.main.transform.position;
        CamPos = Vector3.Lerp(CamPos, new Vector3(transform.position.x, CamPos.y, transform.position.z), CamSpeed * Time.deltaTime);
        Camera.main.transform.position = CamPos;

    }
    private void FixedUpdate()
    {


        if (!isLocalPlayer)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire1();
        }
        CamMovement();
        Rotate();
        Move();
    }

    [Command]
    private void CmdFire1()
    {


        GameObject bullet1 = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        NetworkServer.Spawn(bullet1);
        Debug.Log("Fire!");
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        HealthText.text = Health.ToString();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        HealthText.text = Health.ToString();
    }

    public void tankerHealth(int Damage)
    {
        if (!isServer)
            return;
        Health -= Damage;
        RpcHealthTextChange(Health);
       // if(Health<=)
    }

    [ClientRpc] private void RpcHealthTextChange(int NewHealth)
    {
        Health = NewHealth;
        HealthText.text = Health.ToString();
    }
}
