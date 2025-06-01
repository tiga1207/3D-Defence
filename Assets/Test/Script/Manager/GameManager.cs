// using DesignPattern;
// using UnityEngine;

// public class GameManager : Singleton<GameManager>
// {
//     [SerializeField] private AudioManager _audio;
//     [SerializeField] private GameTimeManager _gameTime;
//     [SerializeField] private UIManager _ui;
//     public AudioManager Audio => _audio;
//     public GameTimeManager GameTime => _gameTime;
//     public UIManager UI => _ui;


//     void Awake()
//     {
//         SingletonInit();
//     }

//     public void RegisterManagers(GameTimeManager gtm, UIManager ui)
//     {
//         _gameTime = gtm;
//         _ui = ui;
//     }
// }