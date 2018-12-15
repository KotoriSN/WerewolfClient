using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventEnum = WerewolfClient.WerewolfModel.EventEnum;
using CommandEnum = WerewolfClient.WerewolfCommand.CommandEnum;
using WerewolfAPI.Model;
using Role = WerewolfAPI.Model.Role;
using WMPLib;
using System.Media;

namespace WerewolfClient
{
    public partial class MainForm_16 : Form, View
    {
        WindowsMediaPlayer player_day = new WindowsMediaPlayer();
        WindowsMediaPlayer player_night = new WindowsMediaPlayer();
        SoundPlayer sp = new SoundPlayer(@"C:\Users\Kotori\Source\Repos\WerewolfClient\WerewolfClient\Resources\PressButton.wav");
        private Timer _updateTimer;
        private WerewolfController controller;
        private Game.PeriodEnum _currentPeriod;
        private int _currentDay;
        private int _currentTime;
        private bool _voteActivated;
        private bool _actionActivated;
        private string _myRole;
        private bool _isDead;
        private List<Player> players = null;
        private Endgame end;

        public MainForm_16()
        {
            player_day.URL = "GameplayBgmusic.mp3";
            player_night.URL = "Nightplaygamemusicbg.mp3";
            InitializeComponent();

            foreach (int i in Enumerable.Range(0, 16))
            {
                this.Controls["BtnPlayer" + i].Click += new System.EventHandler(this.BtnPlayerX_Click);
                this.Controls["BtnPlayer" + i].Tag = i;
                this.Controls["BtnPlayer" + i].Enabled = false;
                this.Controls["BtnPlayer" + i].Text = null;
                ((Button)this.Controls["BtnPlayer" + i]).Image = null;
            }

            _updateTimer = new Timer();
            _voteActivated = false;
            _actionActivated = false;
            EnableButton(BtnAction, false);
            EnableButton(BtnVote, false);
            _myRole = null;
            _isDead = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.PlayGame3333;
            _updateTimer.Interval = 1000;
            _updateTimer.Tick += new EventHandler(OnTimerEvent);
            _updateTimer.Enabled = true;
        }

        public void Notify(Model m)
        {
            if (m is WerewolfModel)
            {
                WerewolfModel wm = (WerewolfModel)m;
                switch (wm.Event)
                {
                    case EventEnum.GameStopped:
                        AddChatMessage("Game is finished, outcome is " + wm.EventPayloads["Game.Outcome"]);
                        _updateTimer.Enabled = false;
                        
                        break;
                    case EventEnum.ExitGame:
                        TbChatBox.Text = null;
                        end.Visible = true;
                        this.Visible = false;
                        break;
                    case EventEnum.GameStarted:
                        players = wm.Players;
                        _myRole = wm.EventPayloads["Player.Role.Name"];
                        AddChatMessage("Your role is " + _myRole + ".");
                        _currentPeriod = Game.PeriodEnum.Night;
                        EnableButton(BtnAction, true);
                        switch (_myRole)
                        {
                            case WerewolfModel.ROLE_PRIEST:
                                BtnAction.Text = WerewolfModel.ACTION_HOLYWATER;
                                break;
                            case WerewolfModel.ROLE_GUNNER:
                                BtnAction.Text = WerewolfModel.ACTION_SHOOT;
                                break;
                            case WerewolfModel.ROLE_JAILER:
                                BtnAction.Text = WerewolfModel.ACTION_JAIL;
                                break;
                            case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                                BtnAction.Text = WerewolfModel.ACTION_ENCHANT;
                                break;
                            case WerewolfModel.ROLE_BODYGUARD:
                                BtnAction.Text = WerewolfModel.ACTION_GUARD;
                                break;
                            case WerewolfModel.ROLE_DOCTOR:
                                BtnAction.Text = WerewolfModel.ACTION_HEAL;
                                break;
                            case WerewolfModel.ROLE_SERIAL_KILLER:
                                BtnAction.Text = WerewolfModel.ACTION_KILL;
                                break;
                            case WerewolfModel.ROLE_SEER:
                            case WerewolfModel.ROLE_WEREWOLF_SEER:
                                BtnAction.Text = WerewolfModel.ACTION_REVEAL;
                                break;
                            case WerewolfModel.ROLE_AURA_SEER:
                                BtnAction.Text = WerewolfModel.ACTION_AURA;
                                break;
                            case WerewolfModel.ROLE_MEDIUM:
                                BtnAction.Text = WerewolfModel.ACTION_REVIVE;
                                break;
                            default:
                                EnableButton(BtnAction, false);
                                break;
                        }
                        EnableButton(BtnVote, true);
                        foreach (int i in Enumerable.Range(0, 16))
                        {
                            Controls["BtnPlayer" + i].Enabled = false;
                            Controls["BtnPlayer" + i].Text = null;
                            ((Button)this.Controls["BtnPlayer" + i]).Image = null;
                        }
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.SwitchToDayTime:
                        AddChatMessage("Switch to day time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Day;
                        player_night.controls.stop();
                        player_day.controls.stop();
                        this.BackgroundImage = Properties.Resources.PlayGame222;
                        break;
                    case EventEnum.SwitchToNightTime:
                        AddChatMessage("Switch to night time of day #" + wm.EventPayloads["Game.Current.Day"] + ".");
                        _currentPeriod = Game.PeriodEnum.Night;
                        player_day.controls.stop();
                        player_night.controls.play();
                        this.BackgroundImage = Properties.Resources.PlayGame3333;
                        break;
                    case EventEnum.UpdateDay:
                        // TODO  catch parse exception here
                        string tempDay = wm.EventPayloads["Game.Current.Day"];
                        _currentDay = int.Parse(tempDay);
                        LBDay.Text = tempDay;
                        break;
                    case EventEnum.UpdateTime:
                        string tempTime = wm.EventPayloads["Game.Current.Time"];
                        _currentTime = int.Parse(tempTime);
                        LBTime.Text = tempTime;
                        UpdateAvatar(wm);
                        break;
                    case EventEnum.Vote:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your vote is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't vote now.");
                        }
                        break;
                    case EventEnum.Action:
                        if (wm.EventPayloads["Success"] == WerewolfModel.TRUE)
                        {
                            AddChatMessage("Your action is registered.");
                        }
                        else
                        {
                            AddChatMessage("You can't perform action now.");
                        }
                        break;
                    case EventEnum.YouShotDead:
                        if (!_isDead) AddChatMessage("You're shot dead by gunner.");
                        _isDead = true;
                        break;
                    case EventEnum.OtherShotDead:
                        AddChatMessage(wm.EventPayloads["Game.Target.Name"] + " was shot dead by gunner.");
                        break;
                    case EventEnum.Alive:
                        if (_isDead)
                        {
                            AddChatMessage("You've been revived by medium.");
                            _isDead = false;
                        }
                        break;
                }
                // need to reset event
                wm.Event = EventEnum.NOP;
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            WerewolfCommand wcmd = new WerewolfCommand();
            wcmd.Action = CommandEnum.RequestUpdate;
            controller.ActionPerformed(wcmd);
        }

        public void AddChatMessage(string str)
        {
            TbChatBox.AppendText(str + Environment.NewLine);
        }

        private void UpdateAvatar(WerewolfModel wm)
        {
            int i = 0;
            foreach (Player player in wm.Players)
            {
                Controls["BtnPlayer" + i].Enabled = true;
                Controls["BtnPlayer" + i].Text = player.Name;
                if (player.Name == wm.Player.Name || player.Status != Player.StatusEnum.Alive)
                {
                    // FIXME, need to optimize this
                    Image img = Properties.Resources.i_Villager;
                    string role;
                    if (player.Name == wm.Player.Name)
                    {
                        role = _myRole;
                    }
                    else if (player.Role != null)
                    {
                        role = player.Role.Name;
                    }
                    else
                    {
                        continue;
                    }
                    switch (role)
                    {
                        default:
                            img = Properties.Resources.i_Villager;
                            break;
                        case WerewolfModel.ROLE_SEER:
                            img = Properties.Resources.i_Seer;
                            break;
                        case WerewolfModel.ROLE_AURA_SEER:
                            img = Properties.Resources.i_Theseer;
                            break;
                        case WerewolfModel.ROLE_PRIEST:
                            img = Properties.Resources.i_Priest;
                            break;
                        case WerewolfModel.ROLE_DOCTOR:
                            img = Properties.Resources.i_Doctor;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF:
                            img = Properties.Resources.i_Werewolf;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SEER:
                            img = Properties.Resources.i_Wolfseer;
                            break;
                        case WerewolfModel.ROLE_ALPHA_WEREWOLF:
                            img = Properties.Resources.i_aWerewolf;
                            break;
                        case WerewolfModel.ROLE_WEREWOLF_SHAMAN:
                            img = Properties.Resources.i_Wolfshaman;
                            break;
                        case WerewolfModel.ROLE_MEDIUM:
                            img = Properties.Resources.i_medium1;
                            break;
                        case WerewolfModel.ROLE_BODYGUARD:
                            img = Properties.Resources.i_Bodyguard;
                            break;
                        case WerewolfModel.ROLE_JAILER:
                            img = Properties.Resources.i_Jailer;
                            break;
                        case WerewolfModel.ROLE_FOOL:
                            img = Properties.Resources.i_Fool;
                            break;
                        case WerewolfModel.ROLE_HEAD_HUNTER:
                            img = Properties.Resources.i_Hunter;
                            break;
                        case WerewolfModel.ROLE_SERIAL_KILLER:
                            img = Properties.Resources.i_Killer;
                            break;
                        case WerewolfModel.ROLE_GUNNER:
                            img = Properties.Resources.i_Gunner;
                            break;
                    }
                    ((Button)Controls["BtnPlayer" + i]).Image = img;
                }
                i++;
            }
        }

        public void setend(Endgame end)
        {
            this.end = end;
        }

        public void setController(Controller c)
        {
            controller = (WerewolfController)c;
        }

        public void EnableButton(Button btn, bool state)
        {
            btn.Visible = btn.Enabled = state;
        }

        private void BtnPlayerX_Click(object sender, EventArgs e)
        {
            Button btnPlayer = (Button)sender;
            int index = (int)btnPlayer.Tag;
            if (players == null)
            {
                // Nothing to do here;
                return;
            }
            if (_actionActivated)
            {
                _actionActivated = false;
                BtnAction.BackColor = Button.DefaultBackColor;
                AddChatMessage("You perform [" + BtnAction.Text + "] on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Action;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
            if (_voteActivated)
            {
                _voteActivated = false;
                BtnVote.BackColor = Button.DefaultBackColor;
                AddChatMessage("You vote on " + players[index].Name);
                WerewolfCommand wcmd = new WerewolfCommand();
                wcmd.Action = CommandEnum.Vote;
                wcmd.Payloads = new Dictionary<string, string>() { { "Target", players[index].Id.ToString() } };
                controller.ActionPerformed(wcmd);
            }
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (_isDead)
            {
                AddChatMessage("You're dead!!");
                return;
            }
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnAction.BackColor = Color.Red;
            }
            _actionActivated = !_actionActivated;
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
                _voteActivated = false;
            }
        }

        private void BtnVote_Click(object sender, EventArgs e)
        {
            if (_voteActivated)
            {
                BtnVote.BackColor = Button.DefaultBackColor;
            }
            else
            {
                BtnVote.BackColor = Color.Red;
            }
            _voteActivated = !_voteActivated;
            if (_actionActivated)
            {
                BtnAction.BackColor = Button.DefaultBackColor;
                _actionActivated = false;
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            _isDead = true;
            AddChatMessage("leaving game");
            TbChatBox.Text = null;
            end.Visible = true;
            this.Visible = false;
            _updateTimer.Enabled = false;
        }
    }
}
