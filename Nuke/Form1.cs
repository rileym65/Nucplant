using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Nuke
{
    public partial class Form1 : Form
    {
        const int HI_SECONDARY_TEMPERATURE = 1200;
        const int SG_MINIMUM_LEVEL = 30;
        const int SG_MAXIMUM_LEVEL = 80;
        const char TRIPPED = 'T';
        const char OFFLINE = 'O';
        const char CONNECTED = 'C';
        const char OPENED = 'O';
        const char CLOSED = 'C';
        const char YES = 'Y';
        const char NO = 'N';
        const char RUNNING = 'R';
        const char STARTED = 'S';
        const int BASE_VOLTAGE = 120;
        const int LOW_PRIMARY_TEMP_WARNING = 500;
        const int MIN_PRIMARY_TEMP = 475;
        const int HIGH_PRIMARY_TEMP_WARNING = 600;
        const int LOW_PRIMARY_FLOW_RATE_WARNING = 99;
        const int LOW_PRIMARY_PRESSURE_WARNING = 2000;
        const int MIN_PRIMARY_PRESSURE = 1900;
        const int LOW_SYNC = 3595;
        const int HIGH_SYNC = 3605;
        const int GENERATOR_OVERPOWER = 700;
        const int LOW_SECONDARY_PRESSURE_WARNING = 1000;
        const int LOW_SECONDARY_TEMP_WARNING = 1000;
        const int LOW_PRIMARY_PRESSURE = 1000;
        const double RATIO_HALF = 5.0 / 10.0;

        public int RODS, RodStep, CntrlRods;
        public int ANC, A;
        public int ALY1, ALY2, ALY3, ALY4, ALY5, ALY;
        public int BTN1, BTN2, BTN3, BTN4, BB, B;
        public int C;
        public int DP, DS;
        public int F1, F2, F3;
        public int HG, HV, JA, JB, JH, JL, JO;
        public int JQ, JU, JV, JW, JZ, JF, JM, HB;
        public int JG, HC, JE;
        public int LA, LB, LC, LD, LE, LF;
        public int PZ, PP;
        public double KA, GM, GN, GO, GP, KB, KC;
        public double GQ, GR, GS, GI, GH, GJ, MP, GL;
        public int OldMwth, OldPower, OldPTemp, OldPPres, OldFlow, OldMass;
        public int OldSTemp, OldSPres, OldSG1, OldSG2, OldSG3;
        public int OldVKV, OldMVA, OldMW, OldMVAR, OldCurrent, OldRPM;
        public char TI;
 //       public int YA, YB, YC, YD, YE, YF, YG, YH, YI, YJ, YK, YL, YM, YN;
        public int[] D, LBa;
        public int S1, S2, S3, ST, SP;
        public int TP, TU;
        public int FP;
        public int ZE, ZB;
        public long Cycles, TotalMW, AvgMW;
        public char[] Buffer;
        public int LoadMode;
        public int LoadCycle;
        public int EventMode;
        public long Score;
        public int PlantStatus;
        public int Warnings;
        public int Rcp1, Rcp2, Rcp3;
        public char Reactor;
        public double RandSeed;
        public int Breakdown;
        public long timeval;
        public int SG_LO_LIMIT = 40;
        public int SG_HI_LIMIT = 70;
        public bool started = false;
        public int SteamGenerator1Level;
        public int SteamGenerator2Level;
        public int SteamGenerator3Level;
        public char TurbineStatus;
        public char Grid;
        public char SteamDumpValve;
        public int PrimaryPORV;
        public int SecondaryPORV;
        public int PrimaryPorvFlow;
        public int SecondaryPorvFlow;
        public int FeedPump1;
        public int FeedPump2;
        public int AuxFeedPump1;
        public int AuxFeedPump2;
        public char Sync;
        public int SecondaryPressure;
        public int SecondaryTemperature;
        public int PrimaryPressure;
        public int PrimaryTemperature;
        public int ReactorMwth;
        public int ReactorPower;
        public int TurbineRpm;
        public int PrimaryFlowRate;
        public int PrimaryMass;
        public int Mva;
        public int Mw;
        public int Current;
        public int Mvar;
        public int VoltageKv;
        public int CoolantFlow1;
        public int CoolantFlow2;
        public int CoolantFlow3;
        public int SgFeedFlow1;
        public int SgFeedFlow2;
        public int SgAuxFeedFlow1;
        public int SgAuxFeedFlow2;
        public int FeedValve1Setting;
        public int FeedValve2Setting;
        public int FeedValve3Setting;
        public int Sg1FeedFlow;
        public int Sg2FeedFlow;
        public int Sg3FeedFlow;
        public int SiSetting;
        public int SteamDumpSetting;
        public int TurbineRpmSetting;
        public int LoadSetting;
        public int VoltageSetting;
        public int SteamDump;
        public int SafetyInjection;
        public char SafetyInjectionValve;
        public int SgTotalFeedFlow;

        public Random rng = new Random();

        public Form1()
        {
            InitializeComponent();
            debug1.Text = "";
            debug2.Text = "";
            debug3.Text = "";
            debug4.Text = "";
            debug5.Text = "";
            D = new int[45];
            LBa = new int[11];
            Buffer = new char[1000];
            w_RxTrip.BackColor = Color.White;
            w_SI.BackColor = Color.White;
            w_RcpTrip.BackColor = Color.White;
            w_SteamDump.BackColor = Color.White;
            w_HiSgLevel.BackColor = Color.White;
            w_LoSgLevel.BackColor = Color.White;
            w_HiStartupRate.BackColor = Color.White;
            w_PriPorv.BackColor = Color.White;
            w_LowPrimaryFlowRate.BackColor = Color.White;
            w_AuxFeed.BackColor = Color.White;
            w_FeedPumpTrip.BackColor = Color.White;
            w_SecondaryPorv.BackColor = Color.White;
            w_HiPrimaryTemperature.BackColor = Color.White;
            w_HiPrimaryPressure.BackColor = Color.White;
            w_ReactorOverpower.BackColor = Color.White;
            w_GeneratorOverpower.BackColor = Color.White;
            w_HiSecondaryTemperature.BackColor = Color.White;
            w_HiSecondaryPressure.BackColor = Color.White;
            w_LoPrimaryTemperature.BackColor = Color.White;
            w_LoPrimaryPressure.BackColor = Color.White;
            w_TurbineTrip.BackColor = Color.White;
            w_SteamFeedMismatch.BackColor = Color.White;
            w_LoSecondaryTemperature.BackColor = Color.White;
            w_LoSecondaryPressure.BackColor = Color.White;
            OutButton.BackColor = Color.LightGreen;
            InButton.BackColor = Color.LightGreen;
            timer1.Enabled = true;
        }

        private void DrawLights_60000(int anc)
        {
            w_LoPrimaryTemperature.BackColor = ((anc & 1) == 1) ? Color.Red : Color.White;
            w_LoSgLevel.BackColor = ((anc & 2) == 2) ? Color.Red : Color.White;
            w_GeneratorOverpower.BackColor = ((anc & 4) == 4) ? Color.Red : Color.White;
            w_AuxFeed.BackColor = ((anc & 8) == 8) ? Color.Red : Color.White;
            w_HiSgLevel.BackColor = ((anc & 16) == 16) ? Color.Red : Color.White;
            w_PriPorv.BackColor = ((anc & 32) == 32) ? Color.Red : Color.White;
            w_FeedPumpTrip.BackColor = ((anc & 64) == 64) ? Color.Red : Color.White;
            w_HiPrimaryTemperature.BackColor = ((anc & 128) == 128) ? Color.Red : Color.White;
        }

        private void DrawLights_61000(int anc)
        {
            if ((anc & 1) == 1) w_TurbineTrip.BackColor = Color.Red;
            else w_TurbineTrip.BackColor = Color.White;

            if ((anc & 2) == 2)
            {
                w_SI.BackColor = Color.Red;
                SiButton.BackColor = Color.Red;
            }
            else
            {
                w_SI.BackColor = Color.White;
                SiButton.BackColor = Color.LightGreen;
            }

            if ((anc & 4) == 4) w_HiSecondaryTemperature.BackColor = Color.Red;
            else w_HiSecondaryTemperature.BackColor = Color.White;

            if ((anc & 8) == 8) w_HiStartupRate.BackColor = Color.Red;
            else w_HiStartupRate.BackColor = Color.White;

            if ((anc & 16) == 16) w_LowPrimaryFlowRate.BackColor = Color.Red;
            else w_LowPrimaryFlowRate.BackColor = Color.White;

            if ((anc & 32) == 32) w_HiPrimaryPressure.BackColor = Color.Red;
            else w_HiPrimaryPressure.BackColor = Color.White;

            if ((anc & 64) == 64) w_SteamFeedMismatch.BackColor = Color.Red;
            else w_SteamFeedMismatch.BackColor = Color.White;

            if ((anc & 128) == 128) w_LoSecondaryTemperature.BackColor = Color.Red;
            else w_LoSecondaryTemperature.BackColor = Color.White;
        }

        private void DrawLights_62000(int anc)
        {
            w_SteamDump.BackColor = ((anc & 1) == 1) ? Color.Red : Color.White;
            w_RcpTrip.BackColor = ((anc & 2) == 2) ? Color.Red : Color.White;
            w_LoPrimaryPressure.BackColor = ((anc & 4) == 4) ? Color.Red : Color.White;
            w_RxTrip.BackColor = ((anc & 8) == 8) ? Color.Red : Color.White;
            w_LoSecondaryPressure.BackColor = ((anc & 16) == 16) ? Color.Red : Color.White;
            w_HiSecondaryPressure.BackColor = ((anc & 32) == 32) ? Color.Red : Color.White;
            w_SecondaryPorv.BackColor = ((anc & 64) == 64) ? Color.Red : Color.White;
            w_ReactorOverpower.BackColor = ((anc & 128) == 128) ? Color.Red : Color.White;
        }

        private void DrawLights_64000(int anc)
        {
            if ((anc & 1) == 1) Rcp2Button.BackColor = Color.LightGreen;
            else Rcp2Button.BackColor = Color.Red;
            if ((anc & 2) == 2) Rcp3Button.BackColor = Color.LightGreen;
            else Rcp3Button.BackColor = Color.Red;
            if ((anc & 4) == 4) RxButton.BackColor = Color.LightGreen;
            else RxButton.BackColor = Color.Red;
            if ((anc & 8) == 8) Fp1Button.BackColor = Color.LightGreen;
            else Fp1Button.BackColor = Color.Red;
            if ((anc & 16) == 16) Afp1Button.BackColor = Color.Red;
            else Afp1Button.BackColor = Color.LightGreen;
            if ((anc & 32) == 32) SteamDumpButton.BackColor = Color.LightGreen;
            else SteamDumpButton.BackColor = Color.Red;
            if ((anc & 64) == 64) SyncLight.BackColor = Color.LightGreen;
            else SyncLight.BackColor = Color.Red;
            if ((anc & 128) == 128) PriPorvButton.BackColor = Color.LightGreen;
            else PriPorvButton.BackColor = Color.Red;
        }

        private void DrawLights_65000(int anc)
        {
            if ((anc & 1) == 1) Afp2Button.BackColor = Color.Red;
            else Afp2Button.BackColor = Color.LightGreen;
            if ((anc & 2) == 2) GridButton.BackColor = Color.LightGreen;
            else GridButton.BackColor = Color.Red;
            if ((anc & 4) == 4) Rcp1Button.BackColor = Color.LightGreen;
            else Rcp1Button.BackColor = Color.Red;
            if ((anc & 8) == 8) Fp2Button.BackColor = Color.LightGreen;
            else Fp2Button.BackColor = Color.Red;
            if ((anc & 16) == 16) TurbineButton.BackColor = Color.LightGreen;
            else TurbineButton.BackColor = Color.Red;
            if ((anc & 32) == 32) SecPorvButton.BackColor = Color.LightGreen;
            else SecPorvButton.BackColor = Color.Red;
            if (RODS == 1 && RodStep == 0) OutButton.BackColor = Color.Red;
            else OutButton.BackColor = Color.LightGreen;
            if (RODS == -1 && RodStep == 0) InButton.BackColor = Color.Red;
            else InButton.BackColor = Color.LightGreen;
        }

        void FindStatus()
        {
            PlantStatus = 0;
            if (SteamGenerator1Level > 10) PlantStatus = 1;  /* SG1 >10% */
            if (SteamGenerator2Level > 10) PlantStatus = 1;  /* SG1 >10% */
            if (SteamGenerator3Level > 10) PlantStatus = 1;  /* SG1 >10% */
            if (Rcp1 > 0) PlantStatus = 1;      /* RCP 1 */
            if (Rcp2 > 0) PlantStatus = 1;      /* RCP 2 */
            if (Rcp3 > 0) PlantStatus = 1;      /* RCP 3 */

            if (Reactor == RUNNING) PlantStatus = 2;   /* Rx */

            if (CntrlRods > 21) PlantStatus = 3;     /* Rods */

            if (Grid == 'C') PlantStatus = 4;   /* Grid on */

        }

        private void Delay(int t)
        {
            Application.DoEvents();
            Thread.Sleep(t);
        }

        public void InitGame()
        {
            int i, j, k;
            sb_Fv1.Value = 100;
            sb_Fv2.Value = 100;
            sb_Fv3.Value = 100;
            sb_SI.Value = 100;
            sb_SteamDump.Value = 100;
            sb_TurbineRpm.Value = 100;
            sb_Load.Value = 700;
            sb_Voltage.Value = 100;
            ALY1 = 0;
            ALY2 = 0;
            ALY3 = 0;
            ALY4 = 0;
            ALY5 = 0;
            HG = 6; HV = 60; JA = 100; JB = 105; JH = 400;
            JL = 625; JO = 950; JQ = 1150;
            JU = 2300; JV = 2350; JW = 3500; JZ = rng.Next(10);
            KA = 500; GM = 1.25; GN = 0.75; GO = 5 / 100; GP = 30 / 255;
            GQ = 100.0/ 255.0; GR = 1000.0/ 255.0; GS = 25.0/ 100.0; JF = 200; JM = 700; HB = 1; JG = 300;
            HC = 2; JE = 135; KB = 300; KC = 200;

            Reactor = TRIPPED; TurbineStatus = TRIPPED; SafetyInjectionValve = TRIPPED; Grid = OFFLINE; SteamDumpValve = CLOSED;
            GI = 31.0/ 100.0; GH = 35.0/ 100.0; GJ = 34.0/ 100.0; MP = 101;

            BTN1 = 0; BTN2 = 0; BTN3 = 0; BTN4 = 0;
            PrimaryPORV = 0; AuxFeedPump1 = 0; SecondaryPORV = 0; AuxFeedPump2 = 0; FeedPump1 = 0; FeedPump2 = 0;
            FP = 0; PP = 0;
            GL = 0.75; LoadSetting = 0; SteamDump = 0; SteamDumpSetting = 0; SiSetting = 0; SafetyInjection = 0; DP = 0; TP = 0;
            VoltageSetting = 0; FeedValve2Setting = 0; FeedValve3Setting = 0; FeedValve1Setting = 0; TurbineRpmSetting = 0; DS = 0;
            F1 = 0; F2 = 0; F3 = 0; S1 = 0; S2 = 0; S3 = 0; ST = 0; SP = 0;
            Sync = NO; TU = 0; BB = 0; TI = ' '; LD = 0; LE = 0; LF = 0; ZB = 0; ZE = 0;
            KC = 0;
            Rcp1 = 0; Rcp2 = 0; Rcp3 = 0;
            Cycles = 0;
            TotalMW = 0;
            AvgMW = 0;
            LoadMode = 0;
            LoadCycle = 0;
            EventMode = 0;
            Score = 0;
            PlantStatus = 0;
            Breakdown = 0;
            for (i = 0; i < 45; i++)
            { D[i] = 0; }
            for (i = 0; i < 11; i++)
            { LBa[i] = 0; }
            SteamGenerator1Level = 0;
            SteamGenerator2Level = 0;
            SteamGenerator3Level = 0;
            SecondaryPressure = 0;
            SecondaryTemperature = 0;
            PrimaryTemperature = 0;
            PrimaryPressure = 0;
            PrimaryTemperature = 0;
            ReactorMwth = 0;
            ReactorPower = 0;
            TurbineRpm = 0;
            PrimaryFlowRate = 0;
            PrimaryMass = 0;
            Mva = 0;
            Mw = 0;
            Mvar = 0;
            CoolantFlow1 = 0;
            CoolantFlow2 = 0;
            CoolantFlow3 = 0;
            PrimaryPorvFlow = 0;
            SecondaryPorvFlow = 0;
            Sg1FeedFlow = 0;
            Sg2FeedFlow = 0;
            Sg3FeedFlow = 0;
            SgTotalFeedFlow = 0;

            OldMwth = 500;
            OldPower = 50;
            OldPTemp = 550;
            OldPPres = 2150;
            OldFlow = 50;
            OldMass = 50;
            OldSTemp = 1100;
            OldSPres = 1100;
            OldSG1 = 50;
            OldSG2 = 50;
            OldSG3 = 50;
            OldVKV = 200;
            OldMVA = 300;
            OldMW = 300;
            OldMVAR = 100;
            OldCurrent = 2500;
            OldRPM = 3600;
            KC = 0; GO = 0;

            for (k = 1; k < 3; k++)
            {
                j = 1;
                for (i = 1; i < 9; i++)
                {
                    DrawLights_60000(j);
                    Delay(10);
                    DrawLights_61000(j);
                    Delay(10);
                    DrawLights_62000(j);
                    Delay(10);
                    DrawLights_64000(j);
                    Delay(10);
                    DrawLights_65000(j);
                    Delay(10);
                    j = j + j + 1;
                }
                j = 255;
                for (i = 1; i < 10; i++)
                {
                    DrawLights_60000(j);
                    Delay(10);
                    DrawLights_61000(j);
                    Delay(10);
                    DrawLights_62000(j);
                    Delay(10);
                    DrawLights_64000(j);
                    Delay(10);
                    DrawLights_65000(j);
                    Delay(10);
                    j /= 2;
                }
            }
            ALY1 = 255; ALY2 = 255; ALY3 = 255; ALY4 = 255; ALY5 = 255; ALY = 255;
            started = true;
        }

        void cycle()
        {
            int i;
            for (i = 0; i < 8; i++)
            {
                A = (int)Math.Pow(i, 2);
                if ((LA & A) != 0) PZ -= 1;
                if ((LB & A) != 0) PZ -= 1;
                if ((LC & A) != 0) PZ -= 1;
            }
            CntrlRods = CntrlRods + RODS;
            if (CntrlRods < 0) CntrlRods = 0;
            if (CntrlRods > 500) CntrlRods = 500;
            /* Rod indicators should go here */
            if (Reactor == TRIPPED) CntrlRods = 0;
            if ((BTN1 & 2) == 2)
            {
                Reactor = TRIPPED;
                CntrlRods = 0;
                Grid = OFFLINE;
                TurbineStatus = TRIPPED;
            }
            if ((BTN1 & 4) == 4) SteamDumpValve = OPENED;
            if ((BTN1 & 8) == 8) PrimaryPORV = 0;
            if ((BTN1 & 16) == 16) SafetyInjectionValve = TRIPPED;
            if ((BTN1 & 32) == 32) SafetyInjectionValve = STARTED;
            if ((BTN1 & 64) == 64) Grid = CONNECTED;
            if ((BTN1 & 128) == 128) AuxFeedPump1 = 0;
            BTN1 = 0;
            if ((BTN2 & 1) == 1) SecondaryPORV = 0;
            if ((BTN2 & 2) == 2) AuxFeedPump2 = 200;
            if ((BTN2 & 4) == 4) SteamDumpValve = CLOSED;
            if ((BTN2 & 8) == 8) FeedPump1 = 375;
            if ((BTN2 & 16) == 16) Reactor = RUNNING;
            if ((BTN2 & 32) == 32) PrimaryPORV = 5;
            if ((BTN2 & 64) == 64) AuxFeedPump2 = 0;
            if ((BTN2 & 128) == 128) Rcp1 = 0;
            BTN2 = 0;
            if ((BTN3 & 1) == 1) Rcp3 = 0;
            if ((BTN3 & 2) == 2) Rcp3 = 34;
            if ((BTN3 & 4) == 4) AuxFeedPump1 = 200;
            if ((BTN3 & 8) == 8) TurbineStatus = RUNNING;
            if ((BTN3 & 16) == 16) FeedPump2 = 375;
            if ((BTN3 & 32) == 32) FeedPump1 = 0;
            if ((BTN3 & 64) == 64) Grid = OFFLINE;
            if ((BTN3 & 128) == 128) Rcp2 = 34;
            BTN3 = 0;
 //           A = BTN4; BTN4 = 0;
            if ((BTN4 & 1) == 1) Rcp1 = 34;
            if ((BTN4 & 2) == 2) SecondaryPORV = 100;
            if ((BTN4 & 4) == 4) FeedPump2 = 0;
            if ((BTN4 & 8) == 8) Rcp2 = 0;
            if ((BTN4 & 16) == 16) TurbineStatus = TRIPPED;
            if ((BTN4 & 32) == 32) CntrlRods = CntrlRods + 1;
            BTN4 = 0;
            if (CntrlRods > 500) CntrlRods = 500;
            if (Reactor == TRIPPED) { CntrlRods = 0; RODS = 0; }
            if (Reactor == TRIPPED) TurbineStatus = TRIPPED;
            if (TurbineStatus == TRIPPED) Grid = OFFLINE;
            if (Rcp2 > 0) ANC = 1; else ANC = 0;
            if (Rcp3 > 0) ANC |= 2;
            if (Reactor == RUNNING) ANC |= 4;
            if (FeedPump1 > 0) ANC |= 8;
            if (AuxFeedPump1 > 0) ANC |= 16;
            if (SteamDumpValve == CLOSED) ANC |= 32;
            if (Sync == YES) ANC |= 64;
            if (PrimaryPORV == 0) ANC |= 128;
            DrawLights_64000(ANC);
            if (AuxFeedPump2 > 0) ANC = 1; else ANC = 0;
            if (Grid == CONNECTED) ANC |= 2;
            if (Rcp1 > 0) ANC |= 4;
            if (FeedPump2 > 0) ANC |= 8;
            if (TurbineStatus == RUNNING) ANC |= 16;
            if (SecondaryPORV == 0) ANC |= 32;
            DrawLights_65000(ANC);



            CoolantFlow1 = CoolantFlow1 + (int)((Rcp1 - CoolantFlow1) * RATIO_HALF);
            CoolantFlow2 = CoolantFlow2 + (int)((Rcp2 - CoolantFlow2) * RATIO_HALF);
            CoolantFlow3 = CoolantFlow3 + (int)((Rcp3 - CoolantFlow3) * RATIO_HALF);
            SgFeedFlow1 = SgFeedFlow1 + (int)((FeedPump1 - SgFeedFlow1) * GN);
            SgFeedFlow2 = SgFeedFlow2 + (int)((FeedPump2 - SgFeedFlow2) * GN);
            SgAuxFeedFlow1 = SgAuxFeedFlow1 + (int)((AuxFeedPump1 - SgAuxFeedFlow1) * GN);
            SgAuxFeedFlow2 = SgAuxFeedFlow2 + (int)((AuxFeedPump2 - SgAuxFeedFlow2) * GN);
            PrimaryPorvFlow = PrimaryPorvFlow + (int)((PrimaryPORV - PrimaryPorvFlow) * RATIO_HALF);
            SecondaryPorvFlow = SecondaryPorvFlow + (int)((SecondaryPORV - SecondaryPorvFlow) * RATIO_HALF);
            if (Grid == OFFLINE) LoadSetting = 0;
            if (SteamDumpValve == OPENED) SteamDump = SteamDumpSetting * 8; else SteamDump = 0;
            D[35] = D[35] + (int)((SteamDump - D[35]) * GL);
            if (SafetyInjectionValve == STARTED) SafetyInjection = (int)(SiSetting * GS); else SafetyInjection = 0;
            D[36] = D[36] + (int)((SafetyInjection - D[36]) * GL);
            FP = 4 + CoolantFlow1 + CoolantFlow2 + CoolantFlow3;
            PrimaryFlowRate = PrimaryFlowRate + (int)((FP - PrimaryFlowRate) * RATIO_HALF);
            MP = MP - PrimaryPorvFlow + D[36] - LBa[1];
            PrimaryMass = PrimaryMass + (int)((MP - PrimaryMass) * RATIO_HALF);
            if (PrimaryMass < 0) PrimaryMass = 0;

            DP = PrimaryFlowRate * PrimaryMass * CntrlRods / JM;
            ReactorMwth = ReactorMwth + (int)((DP - ReactorMwth) * RATIO_HALF);
            TP = (int)(JF * PrimaryFlowRate / (PrimaryMass + .00001) + (ReactorMwth - D[17]));
            PrimaryTemperature = PrimaryTemperature + (int)((TP - PrimaryTemperature) * RATIO_HALF);
            if (PrimaryTemperature < 0) PrimaryTemperature = 0;
            PP = (int)(4 * PrimaryMass * PrimaryTemperature / (PrimaryFlowRate + .00001));
            PrimaryPressure = PrimaryPressure + (int)((PP - PrimaryPressure) * RATIO_HALF);
            if (PrimaryPressure < 0) PrimaryPressure = 0;
            VoltageKv = VoltageSetting + BASE_VOLTAGE;
            Sg2FeedFlow = Sg2FeedFlow + (int)((FeedValve2Setting - Sg2FeedFlow) * RATIO_HALF);
            Mva = Mva + (int)((LoadSetting - Mva) * RATIO_HALF);
            Sg3FeedFlow = Sg3FeedFlow + (int)((FeedValve3Setting - Sg3FeedFlow) * RATIO_HALF);
            Sg1FeedFlow = Sg1FeedFlow + (int)((FeedValve1Setting - Sg1FeedFlow) * RATIO_HALF);
            D[40] = D[40] + (int)((TurbineRpmSetting - D[40]) * GL);
            DS = D[35] + D[40] / JA + Mva + SecondaryPorvFlow - LBa[2];
            D[17] = D[17] + (int)((DS - D[17]) * RATIO_HALF);
            if (D[17] < 0) D[17] = 0;
            if (Sg2FeedFlow < 1) Sg2FeedFlow = 0;
            if (Sg1FeedFlow < 1) Sg1FeedFlow = 0;
            if (Sg3FeedFlow < 1) Sg3FeedFlow = 0;
            SgTotalFeedFlow = SgFeedFlow1 + SgFeedFlow2 + SgAuxFeedFlow1 + SgAuxFeedFlow2;
            F1 = Sg1FeedFlow * SgTotalFeedFlow / JG;
            D[42] = D[42] + (int)((F1 - D[42]) * RATIO_HALF);
            F2 = Sg2FeedFlow * SgTotalFeedFlow / JG;
            D[20] = D[20] + (int)((F2 - D[20]) * RATIO_HALF);
            F3 = Sg3FeedFlow * SgTotalFeedFlow / JG;
            D[21] = D[21] + (int)((F3 - D[21]) * RATIO_HALF);
            S1 = S1 + (int)(D[42] - D[17] * GI);
            SteamGenerator1Level = SteamGenerator1Level + (int)((S1 / SG_MINIMUM_LEVEL - SteamGenerator1Level) * RATIO_HALF);
            S2 = S2 + (int)(D[20] - D[17] * GH);
            SteamGenerator2Level = SteamGenerator2Level + (int)((S2 / SG_MINIMUM_LEVEL - SteamGenerator2Level) * RATIO_HALF);
            S3 = S3 + (int)(D[21] - D[17] * GJ);
            SteamGenerator3Level = SteamGenerator3Level + (int)((S3 / SG_MINIMUM_LEVEL - SteamGenerator3Level) * RATIO_HALF);
            ST = HC * PrimaryTemperature / (SecondaryPorvFlow + HB);
            SecondaryTemperature = SecondaryTemperature + (int)((ST - SecondaryTemperature) * RATIO_HALF);
            if (SecondaryTemperature < 0) SecondaryTemperature = 0;
            SP = (PrimaryPressure / HC) / (SecondaryPorvFlow + HB);
            SecondaryPressure = SecondaryPressure + (int)((SP - SecondaryPressure) * RATIO_HALF);
            if (SecondaryPressure < 0) SecondaryPressure = 0;
            Mvar = VoltageKv - JE + JZ;
            Current = (int)(Mva * 1000 / (VoltageKv + .00001));
            if (Math.Abs(Mvar) > Mva)
            {
                Mvar = 0;
                Mva = 0;
                Current = 0;
                Mw = 0;
            }
            Mw = (int)(Math.Sqrt((Mva * Mva) - (Mvar * Mvar)));
            if (Grid == OFFLINE)
            {
                Mvar = 0;
                Mva = 0;
                Current = 0;
                Mw = 0;
            }
            if (TurbineStatus == TRIPPED) TU = 0; else TU = JW + 2 * D[40];
            TurbineRpm = TurbineRpm + (int)((TU - TurbineRpm) * RATIO_HALF);
            if (TurbineRpm > LOW_SYNC && TurbineRpm < HIGH_SYNC) Sync = YES; else Sync = NO;
            if (Sync == NO) Grid = OFFLINE;
            ReactorPower = (int)(ReactorMwth / HG - HV);
            if (ReactorPower < 0) ReactorPower = 0;
            if (BB != 0)
                D[43] = (int)(((ReactorMwth - BB) / BB) * JA);
            BB = ReactorMwth;
            PZ = PZ + (int)(Mw * GO);
            if (Reactor == TRIPPED)
            {
                PZ = 0;
                if (ReactorMwth < 1) ReactorMwth = 0;
            }
            if (PZ < 0) PZ = 0;
            m_ReactorMwth.Value((int)(ReactorMwth / 11.11111));
            m_ReactorPower.Value((int)(ReactorPower / 1.3333));
            m_TurbineRpm.Value((int)((TurbineRpm - 3575) / 0.5555));
            m_MVA.Value((int)(Mva / 7.2222));
            m_VoltageKV.Value((int)(VoltageKv / 5.6888));
            m_PrimaryTemperature.Value((int)((PrimaryTemperature - 400)/3.3333));
            m_MVAR.Value((int)(48 + (Mvar * 5)));
            m_PrimaryPressure.Value((int)((PrimaryPressure - 1800)/7.7777));
            m_MW.Value((int)(Mw / 7.2222));
            m_PrimaryFlowRate.Value((int)(PrimaryFlowRate / 1.3333));
            m_Current.Value((int)(Current / 61.1111));
            m_PrimaryMass.Value((int)(PrimaryMass / 1.3333));
            m_Sg1.Value(SteamGenerator1Level);
            m_Sg2.Value(SteamGenerator2Level);
            m_Sg3.Value(SteamGenerator3Level);
            m_SecondaryTemperature.Value((int)((SecondaryTemperature - 800)/6.6666));
            m_SecondaryPressure.Value((int)((SecondaryPressure - 800)/6.6666));
            Warnings = 0;
            if (PrimaryTemperature < LOW_PRIMARY_TEMP_WARNING) { ANC = 1; Warnings++; } else ANC = 0;
            if (PrimaryTemperature < MIN_PRIMARY_TEMP && TI == RUNNING) Reactor = TRIPPED;
            if (SteamGenerator1Level < SG_LO_LIMIT || SteamGenerator2Level < SG_LO_LIMIT || SteamGenerator3Level < SG_LO_LIMIT)
            { ANC ^= 2; Warnings++; }
            ZB = ANC & 2;
            if (Mva > GENERATOR_OVERPOWER) { ANC ^= 4; Warnings++; }
            if (AuxFeedPump1 > 0 || AuxFeedPump2 > 0) { ANC ^= 8; Warnings++; }
            if (SteamGenerator1Level > SG_HI_LIMIT || SteamGenerator2Level > SG_HI_LIMIT || SteamGenerator3Level > SG_HI_LIMIT)
            { ANC ^= 16; Warnings++; }
            ZE = ANC & 16;
            if (PrimaryPORV > 0) { ANC ^= 32; Warnings++; }
            if (FeedPump1 == 0 || FeedPump2 == 0) { ANC ^= 64; Warnings++; }
            if (PrimaryTemperature > HIGH_PRIMARY_TEMP_WARNING) { ANC ^= 128; Warnings++; }
            DrawLights_60000(ANC);
            if (TurbineStatus == TRIPPED) { ANC = 1; Warnings++; } else ANC = 0;
            if (SafetyInjectionValve == STARTED) { ANC ^= 2; Warnings++; }
            if (SecondaryTemperature > HI_SECONDARY_TEMPERATURE) { ANC ^= 4; Warnings++; }
            if (D[43] > JA) { ANC ^= 8; Warnings++; }
            if (PrimaryFlowRate < LOW_PRIMARY_FLOW_RATE_WARNING) { ANC ^= 16; Warnings++; }
            if (PrimaryPressure > JU) { ANC ^= 32; Warnings++; }
            A = D[42] + D[20] + D[21];
            B = (int)(GM * A);
            C = (int)(GN * A);
            if (D[17] > B || D[17] < C) { ANC ^= 64; Warnings++; }
            if (SecondaryTemperature < LOW_SECONDARY_TEMP_WARNING) { ANC ^= 128; Warnings++; }
            DrawLights_61000(ANC);
            if (SteamDumpValve == OPENED) { ANC = 1; Warnings++; } else ANC = 0;
            if (PrimaryPressure < LOW_PRIMARY_PRESSURE_WARNING) { ANC ^= 4; Warnings++; }
            if (PrimaryPressure < MIN_PRIMARY_PRESSURE && TurbineStatus == RUNNING) Reactor = TRIPPED;
            if (SecondaryTemperature < JO || SecondaryPressure < JO) TurbineStatus = TRIPPED;
            if (Rcp1 == 0 || Rcp2 == 0 || Rcp3 == 0) { ANC ^= 2; Warnings++; }
            if (SecondaryPressure < LOW_SECONDARY_PRESSURE_WARNING) { ANC ^= 16; Warnings++; }
            if (SecondaryPressure > JQ) { ANC ^= 32; Warnings++; }
            if (SecondaryPORV > 0) { ANC ^= 64; Warnings++; }
            if (ReactorPower > JA) { ANC ^= 128; Warnings++; }
            if (PrimaryPressure < LOW_PRIMARY_PRESSURE || PrimaryTemperature < JH) SteamDumpValve = CLOSED;
            if (SteamGenerator1Level < SG_MINIMUM_LEVEL || SteamGenerator2Level < SG_MINIMUM_LEVEL || SteamGenerator3Level < SG_MINIMUM_LEVEL || 
                D[43] > JA || ReactorPower > JB || PrimaryTemperature > JL ||
                PrimaryPressure > JV || Reactor == TRIPPED) { ANC ^= 8; Warnings++; }
            if (SteamGenerator1Level > SG_MAXIMUM_LEVEL || SteamGenerator2Level > SG_MAXIMUM_LEVEL || SteamGenerator3Level > SG_MAXIMUM_LEVEL) TurbineStatus = TRIPPED;
            DrawLights_62000(ANC);
            LD = LA;
            LE = LB;
            LF = LC;
            if (ZE == 16)
            {
                FeedPump1 = 0;
                FeedPump2 = 0;
                AuxFeedPump1 = 0;
                AuxFeedPump2 = 0;
            }
            if (ZB == 2)
            {
                AuxFeedPump1 = 200;
                AuxFeedPump2 = 200;
                FeedPump2 = 375;
                FeedPump1 = 375;
            }
            if ((ANC & 8) == 8)
            {
                Reactor = TRIPPED;
                TurbineStatus = TRIPPED;
                Grid = OFFLINE;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (started)
            {
                cycle();
                if (RodStep == 1 && RODS != 0) RODS = 0;
                RodStep = 0;
                Cycles++;
                lb_MwOut.Text = Mw.ToString();
                TotalMW += Mw;
                AvgMW = (int)(TotalMW / Cycles);
                if (Cycles < 600)
                {
                    Score += AvgMW;
                    if (Mw >= 600) Score += AvgMW;
                    Score -= (Warnings * 100);
                    if (Score < 0) Score = 0;
                    lb_Score.Text = Score.ToString();
                }
                lb_AverageMW.Text = AvgMW.ToString();
                lb_Time.Text = Cycles.ToString();
                FindStatus();
                switch (PlantStatus)
                {
                    case 0: lb_Status.Text = "Cold"; break;
                    case 1: lb_Status.Text = "Startup"; break;
                    case 2: lb_Status.Text = "Warm"; break;
                    case 3: lb_Status.Text = "Hot"; break;
                    case 4: lb_Status.Text = "Online"; break;
                }
            }
        }

        private void InButton_Click(object sender, EventArgs e)
        {
            if (RODS == 0)
            {
                RODS = -1;
                RodStep = 0;
            }
            else
            {
                RODS = 0;
                RodStep = 0;
            }
        }

        private void Rcp1Button_Click(object sender, EventArgs e)
        {
            if (Rcp1Button.BackColor == Color.Red) BTN4 ^= 1; else BTN2 ^= 128;
        }

        private void Rcp2Button_Click(object sender, EventArgs e)
        {
            if (Rcp2Button.BackColor == Color.Red) BTN3 ^= 128; else BTN4 ^= 8;
        }

        private void Rcp3Button_Click(object sender, EventArgs e)
        {
            if (Rcp3Button.BackColor == Color.Red) BTN3 ^= 2; else BTN3 ^= 1;
        }

        private void PriPorvButton_Click(object sender, EventArgs e)
        {
            if (PriPorvButton.BackColor == Color.Red) BTN1 ^= 8; else BTN2 ^= 32;
        }

        private void SiButton_Click(object sender, EventArgs e)
        {
            if (SiButton.BackColor == Color.Red) BTN1 ^= 16; else BTN1 ^= 32;
        }

        private void RxButton_Click(object sender, EventArgs e)
        {
            if (RxButton.BackColor == Color.Red) BTN2 ^= 16; else BTN1 ^= 2;
        }

        private void OutButton_Click(object sender, EventArgs e)
        {
            if (RODS == 0)
            {
                RODS = 1;
                RodStep = 0;
            }
            else
            {
                RODS = 0;
                RodStep = 0;
            }
        }


        private void Out1Button_Click(object sender, EventArgs e)
        {
            RODS = 1;
            RodStep = 1;
        }

        private void In1Button_Click(object sender, EventArgs e)
        {
            RODS = -1;
            RodStep = 1;
        }

        private void sb_SI_ValueChanged(object sender, EventArgs e)
        {
            SiSetting = 100 - sb_SI.Value;
        }

        private void Fp1Button_Click(object sender, EventArgs e)
        {
            if (Fp1Button.BackColor == Color.Red) BTN2 ^= 8; else BTN3 ^= 32;
        }

        private void Fp2Button_Click(object sender, EventArgs e)
        {
            if (Fp2Button.BackColor == Color.Red) BTN3 ^= 16; else BTN4 ^= 4;
        }

        private void Afp1Button_Click(object sender, EventArgs e)
        {
            if (Afp1Button.BackColor == Color.Red) BTN1 ^= 128; else BTN3 ^= 4;
        }

        private void Afp2Button_Click(object sender, EventArgs e)
        {
            if (Afp2Button.BackColor == Color.Red) BTN2 ^= 64; else BTN2 ^= 2;
        }

        private void sb_Fv1_ValueChanged(object sender, EventArgs e)
        {
            FeedValve1Setting = 100 - sb_Fv1.Value;
        }

        private void sb_Fv2_ValueChanged(object sender, EventArgs e)
        {
            FeedValve2Setting = 100 - sb_Fv2.Value;
        }

        private void sb_Fv3_ValueChanged(object sender, EventArgs e)
        {
            FeedValve3Setting = 100 - sb_Fv3.Value;
        }

        private void sb_SteamDump_ValueChanged(object sender, EventArgs e)
        {
            SteamDumpSetting = 100 - sb_SteamDump.Value;
        }

        private void SteamDumpButton_Click(object sender, EventArgs e)
        {
            if (SteamDumpButton.BackColor == Color.Red) BTN2 ^= 4; else BTN1 ^= 4;
        }

        private void SecPorvButton_Click(object sender, EventArgs e)
        {
            if (SecPorvButton.BackColor == Color.Red) BTN2 ^= 1; else BTN4 ^= 2;
        }

        private void TurbineButton_Click(object sender, EventArgs e)
        {
            if (TurbineButton.BackColor == Color.Red) BTN3 ^= 8; else BTN4 ^= 16;
        }

        private void sb_TurbineRpm_ValueChanged(object sender, EventArgs e)
        {
            TurbineRpmSetting = 100 - sb_TurbineRpm.Value;
        }

        private void GridButton_Click(object sender, EventArgs e)
        {
            if (GridButton.BackColor == Color.Red) BTN1 ^= 64; else BTN3 ^= 64;
        }

        private void sb_Load_ValueChanged(object sender, EventArgs e)
        {
            LoadSetting = 700 - sb_Load.Value;
        }

        private void sb_Voltage_ValueChanged(object sender, EventArgs e)
        {
            VoltageSetting = (100 - sb_Voltage.Value) / 5;

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitGame();
        }
    }
}
