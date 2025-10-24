  

using System;
using TMPro;
using UnityEngine;

public class MAXReward : MonoBehaviour
{
#if UNITY_IOS
     string rewardedAdUnitId = "6153b41caf275f07"; // IOS
#else
  string rewardedAdUnitId = "d54086bf10c91a64"; // Android
#endif


  int retryAttempt;
  private int _idRewardButton;

  private AppMetEvents _appMetEvents;
  private int _internetConnection = 1;
  [SerializeField] private AdsButton _boosts;
  private SpellsWrapper _spellWrapper;
  private Rampage _rampage;
  private QuestManager _questManager;
  private EarnBuild _earnOffline;
  [SerializeField] private ButtonInfo[] _buttonInfo;
  [SerializeField] private TowerWrapper _towerWrapper;
  [SerializeField] private WarriorsCoreUpgrade[] _warriorsUpgrades;
  [SerializeField] private UpgradeDetails[] _upgradesDetails;
  [SerializeField] private TextMeshProUGUI _rewardMoneyDisplayText;
  [SerializeField] private AddWarriors _addWarriors;
  [SerializeField] private RandomSpell _randomSpell;
  private GameUI _gameUI;
  private Wallet _wallet;
  private static int rewardMoneyCounter;
  [HideInInspector] public int RandomChestOfGold;

  private void Awake()
  {
    SetRandomMoney();
  }

  private void Start()
  {
    //_appMetEvents = FindObjectOfType<AppMetEvents>();
    _wallet = FindObjectOfType<Wallet>();
    _spellWrapper = FindObjectOfType<SpellsWrapper>();
    _rampage = FindObjectOfType<Rampage>();
    _questManager = FindObjectOfType<QuestManager>();
    _earnOffline = FindObjectOfType<EarnBuild>();
    _gameUI = FindObjectOfType<GameUI>();

    //MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
    //{
    //  // AppLovin SDK is initialized, configure and start loading ads.
    //  Debug.Log("MAX SDK Initialized");
    //  InitializeRewardedAds();
    //};

    //MaxSdk.SetSdkKey("OP7huuk2dHdSduPkl8rGi8KhMnCZ8ewBCsRxzs_DD2U6lxranzNZ8SLzXwSHrLXoPnwNNFIsCtsvQ-iSV3DG8J");
    //MaxSdk.InitializeSdk();
  }


  private void SetRandomMoney()
  {
    RandomChestOfGold = UnityEngine.Random.Range(800, 2500);
  }

  public void InitializeRewardedAds()
  {
    // Attach callback
    MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
    MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
    MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
    MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
    MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
    MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
    MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

    //  // Load the first RewardedAd
    LoadRewardedAd();
  }

  private void OnDisable()
  {
    MaxSdkCallbacks.OnSdkInitializedEvent -= sdkConfiguration =>
    {
      // AppLovin SDK is initialized, configure and start loading ads.
      Debug.Log("MAX SDK Initialized");
      InitializeRewardedAds();
    };

    MaxSdkCallbacks.OnRewardedAdLoadedEvent -= OnRewardedAdLoadedEvent;
    MaxSdkCallbacks.OnRewardedAdLoadFailedEvent -= OnRewardedAdFailedEvent;
    MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent -= OnRewardedAdFailedToDisplayEvent;
    MaxSdkCallbacks.OnRewardedAdDisplayedEvent -= OnRewardedAdDisplayedEvent;
    MaxSdkCallbacks.OnRewardedAdClickedEvent -= OnRewardedAdClickedEvent;
    MaxSdkCallbacks.OnRewardedAdHiddenEvent -= OnRewardedAdDismissedEvent;
    MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent -= OnRewardedAdReceivedRewardEvent;
  }

  private void LoadRewardedAd()
  {
    MaxSdk.LoadRewardedAd(rewardedAdUnitId);
  }

  private void OnRewardedAdLoadedEvent(string adUnitId)
  {
    // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

    // Reset retry attempt
    retryAttempt = 0;
  }

  private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
  {
    // Rewarded ad failed to load 
    // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

    retryAttempt++;
    double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

    Invoke("LoadRewardedAd", (float) retryDelay);
  }

  private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
  {
    // Rewarded ad failed to display. We recommend loading the next ad
    LoadRewardedAd();
  }

  private void OnRewardedAdDisplayedEvent(string adUnitId)
  {
  }

  private void OnRewardedAdClickedEvent(string adUnitId)
  {
    //EventWatched("clicked");
  }

  private void OnRewardedAdDismissedEvent(string adUnitId)
  {
    // Rewarded ad is hidden. Pre-load the next ad
    LoadRewardedAd();
    //EventWatched("canceled");
  }

  public void AdsGetReward(int _id)
  {
    if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
      _internetConnection = 1;
    else
      _internetConnection = 0;

    Debug.Log("INTERNET" + _internetConnection);

    if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
    {
      _idRewardButton = _id;

      switch (_idRewardButton)
      {
        case 1:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_timeX2", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_timeX2", _internetConnection);
          break;

        case 2:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_goldx3", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_goldx3", _internetConnection);
          break;

        case 3:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_Free_gold", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_Free_gold", _internetConnection);
          break;

        case 4:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_10_gems", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_10_gems", _internetConnection);
          break;

        case 5:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_heal", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_heal", _internetConnection);
          break;

        case 6:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_mines", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_mines", _internetConnection);
          break;

        case 7:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_meteor", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_meteor", _internetConnection);
          break;

        case 8:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_bomber", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_bomber", _internetConnection);
          break;

        case 9:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_stun", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_stun", _internetConnection);
          break;

        case 10:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_poison", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_poison", _internetConnection);
          break;

        case 11:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_spell_ice", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_spell_ice", _internetConnection);
          break;

        case 12:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_rampage_1stLine", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_rampage_1stLine", _internetConnection);
          break;

        case 13:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_rampage_2ndLine", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_rampage_2ndLine", _internetConnection);
          break;

        case 14:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_continueGame", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_continueGame", _internetConnection);
          break;

        case 15:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_1stLine", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_1stLine", _internetConnection);
          break;

        case 16:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_2ndLine", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_2ndLine", _internetConnection);
          break;

        case 17:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_offlineEarn", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_offlineEarn", _internetConnection);
          break;

        case 18:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_-1hr_offlineEarn", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_-1hr_offlineEarn", _internetConnection);
          break;

        case 19:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_goldForKill", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_goldForKill", _internetConnection);
          break;

        case 20:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_-1hr_goldForKill", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_-1hr_goldForKill", _internetConnection);
          break;

        case 21:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_durationRampage", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_durationRampage", _internetConnection);
          break;

        case 22:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_-1hr_durationRampage", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_-1hr_durationRampage", _internetConnection);
          break;

        case 23:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_damageBoosts", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_damageBoosts", _internetConnection);
          break;

        case 24:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_-1hr_damageBoosts", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_-1hr_damageBoosts", _internetConnection);
          break;

        case 25:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_freeUpgrade_tapDamage", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_freeUpgrade_tapDamage", _internetConnection);
          break;

        case 26:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_-1hr_tapDamage", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_-1hr_tapDamage", _internetConnection);
          break;

        case 27:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_X2_earnOffline", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_X2_earnOffline", _internetConnection);
          break;

        case 28:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_tower_heal", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_tower_heal", _internetConnection);
          break;

        case 29:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_tower_attack", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_tower_attack", _internetConnection);
          break;

        case 30:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_tower_money", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_tower_money", _internetConnection);
          break;

        case 31:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_add_warrior", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_add_warrior", _internetConnection);
          break;

        case 32:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_pickUp_ChestOfGold", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_pickUp_ChestOfGold", _internetConnection);
          break;

        case 33:
          _appMetEvents.VideoAdsAvailable("rewarded", "get_random_boost", _internetConnection);
          _appMetEvents.VideoAdsStartded("rewarded", "get_random_boost", _internetConnection);
          break;

        default:
          break;
      }

      MaxSdk.ShowRewardedAd(rewardedAdUnitId);
    }
  }

  private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
  {
    switch (_idRewardButton)
    {
      //Boost Timer X2
      case 1:
        _boosts.UseAdsSpell(0);
        //_appMetEvents.VideoAdsWatch("rewarded", "watched", "get_timeX2", _internetConnection);
        break;

      //Boost Gold X3
      case 2:
        _boosts.UseAdsSpell(1);
        //_appMetEvents.VideoAdsWatch("rewarded", "watched", "get_goldx3", _internetConnection);
        break;

      // 1K gold
      case 3:
        _wallet.SetMoney(1000 * (rewardMoneyCounter + 1));

        if (rewardMoneyCounter < 10)
          rewardMoneyCounter++;

        _rewardMoneyDisplayText.text = "+" + (rewardMoneyCounter + 1) + "K";
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_Free_gold", _internetConnection);
        break;

      // 10 gems
      case 4:
        _wallet.SetGems(10);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_10_gems", _internetConnection);
        break;

      case 5:
        _spellWrapper.AdsSpells(0);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_heal", _internetConnection);
        break;

      case 6:
        _buttonInfo[0].AdsButtonChecker(false);
        _spellWrapper.AdsSpells(1);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_mines", _internetConnection);
        break;

      case 7:
        _spellWrapper.AdsSpells(2);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_meteor", _internetConnection);
        break;

      case 8:
        _spellWrapper.AdsSpells(4);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_bomber", _internetConnection);
        break;

      case 9:
        _spellWrapper.AdsSpells(6);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_stun", _internetConnection);
        break;

      case 10:
        _buttonInfo[1].AdsButtonChecker(false);
        _spellWrapper.AdsSpells(7);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_poison", _internetConnection);
        break;

      case 11:
        _spellWrapper.AdsSpells(8);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_spell_ice", _internetConnection);
        break;

      case 12:
        _rampage.AdsRampage(0);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_rampage_1stLine", _internetConnection);
        break;

      case 13:
        _rampage.AdsRampage(1);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_rampage_2ndLine", _internetConnection);
        break;

      case 14:
        //_gameUI.AddGameCounter(-1);
        _questManager.AdsPlayerContinue();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_continueGame", _internetConnection);
        break;

      case 15:
        _warriorsUpgrades[0].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_1stLine", _internetConnection);
        break;

      case 16:
        _warriorsUpgrades[1].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_2ndLine", _internetConnection);
        break;

      case 17:
        _upgradesDetails[0].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_offlineEarn", _internetConnection);
        break;

      case 18:
        _upgradesDetails[0].AdsUpgradeProcess();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_-1hr_offlineEarn", _internetConnection);
        break;

      case 19:
        _upgradesDetails[1].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_goldForKill", _internetConnection);
        break;

      case 20:
        _upgradesDetails[1].AdsUpgradeProcess();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_-1hr_goldForKill", _internetConnection);
        break;

      case 21:
        _upgradesDetails[2].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_durationRampage", _internetConnection);
        break;

      case 22:
        _upgradesDetails[2].AdsUpgradeProcess();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_-1hr_durationRampage", _internetConnection);
        break;

      case 23:
        _upgradesDetails[3].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_damageBoosts", _internetConnection);
        break;

      case 24:
        _upgradesDetails[3].AdsUpgradeProcess();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_-1hr_damageBoosts", _internetConnection);
        break;

      case 25:
        _upgradesDetails[4].UseUpgrade();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_freeUpgrade_tapDamage", _internetConnection);
        break;

      case 26:
        _upgradesDetails[4].AdsUpgradeProcess();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_-1hr_tapDamage", _internetConnection);
        break;

      case 27:
        _earnOffline.AdsCollectX2();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_X2_earnOffline", _internetConnection);
        break;

      case 28:
        _towerWrapper.UseTower(0);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_tower_heal", _internetConnection);
        break;

      case 29:
        _towerWrapper.UseTower(1);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_tower_attack", _internetConnection);
        break;

      case 30:
        _towerWrapper.UseTower(2);
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_tower_money", _internetConnection);
        break;

      case 31:
        _addWarriors.AdsAddUnity();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_add_warrior", _internetConnection);
        break;

      case 32:
        _wallet.SetMoney(RandomChestOfGold);
        SetRandomMoney();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_pickUp_ChestOfGold", _internetConnection);
        break;

      case 33:
        _randomSpell.AdsUpdateSpell();
        _appMetEvents.VideoAdsWatch("rewarded", "watched", "get_random_boost", _internetConnection);
        break;

      default:
        break;
    }

    _idRewardButton = 0;
  }
}
