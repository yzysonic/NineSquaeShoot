using UnityEngine;
using System.Collections;

///
/// !!! Machine generated code !!!
/// !!! DO NOT CHANGE Tabs to Spaces !!!
/// 
[System.Serializable]
public class StageDataInUnityData
{
  [SerializeField]
  int n_id;
  public int N_ID { get {return n_id; } set { n_id = value;} }
  
  [SerializeField]
  int n_round;
  public int N_Round { get {return n_round; } set { n_round = value;} }
  
  [SerializeField]
  int[] an_roundgroup = new int[0];
  public int[] An_Roundgroup { get {return an_roundgroup; } set { an_roundgroup = value;} }
  
  [SerializeField]
  int n_rewardcoin;
  public int N_Rewardcoin { get {return n_rewardcoin; } set { n_rewardcoin = value;} }
  
  [SerializeField]
  int n_rewardtype;
  public int N_Rewardtype { get {return n_rewardtype; } set { n_rewardtype = value;} }
  
  [SerializeField]
  int n_rewardrare;
  public int N_Rewardrare { get {return n_rewardrare; } set { n_rewardrare = value;} }
  
  [SerializeField]
  string an_stagemyselfbuff;
  public string An_Stagemyselfbuff { get {return an_stagemyselfbuff; } set { an_stagemyselfbuff = value;} }
  
  [SerializeField]
  string an_stageenemybuff;
  public string An_Stageenemybuff { get {return an_stageenemybuff; } set { an_stageenemybuff = value;} }
  
}