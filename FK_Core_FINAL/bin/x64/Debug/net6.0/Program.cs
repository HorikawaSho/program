using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using FK_CLI;

var origin = new fk_Vector(0.0, 0.0, 0.0);

//ステージ関係
    // 海のシーンの設定
    var SeaScene = new fk_Scene();
    SeaScene.BlendStatus = true;
    SeaScene.BGColor = new fk_Color(0.9, 0.9, 1.0);

    // 海のモデル
    var SeaPlaneM = new fk_Model();
    var SeaSlopeM = new fk_Model();
    var SeaWaterM = new fk_Model();

    var SeaPlane = new fk_Block(500, 5, 100);
    var SeaSlope = new fk_Block(500, 5, 500);
    var SeaWater = new fk_Block(500, 100, 1000);
    
    SeaPlaneM.Shape = SeaPlane;
    SeaSlopeM.Shape = SeaSlope;
    SeaWaterM.Shape = SeaWater;

    SeaPlaneM.GlMoveTo(0, 0, 50);
    SeaSlopeM.GlMoveTo(0, -39, -246);
    SeaWaterM.GlMoveTo(0, -50, -500);
    
    SeaSlopeM.LoAngle(0.0, -Math.PI / 20, 0.0);
    
    var SeaPlaneColor = new fk_Material();
    SeaPlaneColor.Ambient = SeaPlaneColor.Diffuse = new fk_Color(1.0, 1.0, 0.9);
    SeaPlaneM.Material = SeaSlopeM.Material = SeaPlaneColor;

    // 水のマテリアルの設定
    var WaterMaterial = new fk_Material();
    WaterMaterial.Alpha = 0.3f;
    WaterMaterial.Ambient = new fk_Color(0.5, 0.5, 0.8);
    SeaWaterM.Material = WaterMaterial;

    // 海のモデルをシーンに登録
    SeaScene.EntryModel(SeaPlaneM);
    SeaScene.EntryModel(SeaSlopeM);
    SeaScene.EntryModel(SeaWaterM);


//魚モデル
    // イカのモデル
    var SquidHeadM = new fk_Model();
    var SquidBodyM = new fk_Model();
    var SquidEyeM1 = new fk_Model();
    var SquidEyeM2 = new fk_Model();
    var SquidLegM = new fk_Model[11];
    
    var SquidHead = new fk_Prism(3, 3, 3, 2);
    var SquidBody = new fk_Cone(100, 4, 10);
    var SquidEye = new fk_Sphere(100, 1);
    var SquidLeg = new fk_Block(1, 7, 1);
    
    SquidHeadM.Shape = SquidHead;
    SquidBodyM.Shape = SquidBody;
    SquidEyeM1.Shape = SquidEye;
    SquidEyeM2.Shape = SquidEye;
    int s;
    for(s = 0; s<11; s++)
    {
        SquidLegM[s] = new fk_Model();
        SquidLegM[s].Shape = SquidLeg;
    }
    
    SquidHeadM.GlMoveTo(0, 19, -1);
    SquidBodyM.GlMoveTo(0, 10, 0);
    SquidEyeM1.GlMoveTo(2, 11, 3);
    SquidEyeM2.GlMoveTo(-2, 11, 3);
    SquidLegM[0].GlMoveTo(0, 7, 1.5);
    SquidLegM[1].GlMoveTo(1.5, 7, 0.75);
    SquidLegM[2].GlMoveTo(3.0, 7, 0.0);
    SquidLegM[3].GlMoveTo(2.8, 7, -1.5);
    SquidLegM[4].GlMoveTo(1.5, 7, -1.9);
    SquidLegM[5].GlMoveTo(0, 7, -2.5);
    SquidLegM[6].GlMoveTo(-1.5, 7, -2.5);
    SquidLegM[7].GlMoveTo(-2.7, 7, -2);
    SquidLegM[8].GlMoveTo(-3.0, 7, 0);
    SquidLegM[9].GlMoveTo(-1.5, 7, 1.5);
    SquidLegM[10].GlMoveTo(-0.75, 7, 0);
    
    SquidHeadM.LoAngle(0, 0, Math.PI/2);
    SquidHeadM.LoAngle(Math.PI, 0, 0);
    SquidBodyM.LoFocus(0, 1.0, 0);
    SquidLegM[0].LoAngle(0, -Math.PI / 30, 0);
    SquidLegM[1].LoAngle(0, -Math.PI / 30, -Math.PI / 50);
    SquidLegM[2].LoAngle(0, -Math.PI / 30, -Math.PI / 40);
    SquidLegM[3].LoAngle(0, -Math.PI / 30, -Math.PI / 40);
    SquidLegM[4].LoAngle(0, Math.PI / 40, -Math.PI / 50);
    SquidLegM[5].LoAngle(0, Math.PI / 30, -Math.PI / 40);
    SquidLegM[6].LoAngle(0, Math.PI / 30, 0);
    SquidLegM[7].LoAngle(0, Math.PI / 40, Math.PI / 50);
    SquidLegM[8].LoAngle(0, Math.PI / 30, 0);
    SquidLegM[9].LoAngle(0, -Math.PI / 30, Math.PI / 50);
    SquidLegM[10].LoAngle(0, -Math.PI / 30, Math.PI / 50);
    
    var SquidBodyColor = new fk_Material();
    SquidBodyColor.Ambient = SquidBodyColor.Diffuse = new fk_Color(0.9, 0.9, 0.9);
    SquidHeadM.Material = SquidBodyM.Material = SquidBodyColor;
    var SquidLegColor = new fk_Material();
    SquidLegColor.Ambient = SquidLegColor.Diffuse = new fk_Color(0.85, 0.85, 0.85);
    for (s = 0; s < 11; s++)
    {
        SquidLegM[s].Material= SquidLegColor;
    }

    SquidBodyM.SetParent(SquidHeadM, true);
    SquidEyeM1.SetParent(SquidHeadM, true);
    SquidEyeM2.SetParent(SquidHeadM, true);
    for (s = 0; s < 11; s++)
    {
        SquidLegM[s].SetParent(SquidHeadM, true);
    }

    // タコのモデル
    var OctHeadM = new fk_Model();
    var OctEyeM1 = new fk_Model();
    var OctEyeM2 = new fk_Model();
    var OctMouthM = new fk_Model();
    var OctLegM = new fk_Model[11];
    
    var OctHead = new fk_Sphere(100, 5);
    var OctEye = new fk_Sphere(100, 1);
    var OctMouth = new fk_Prism(100, 1, 1, 5);
    var OctLeg = new fk_Block(1, 7, 1);
    
    OctHeadM.Shape = OctHead;
    OctEyeM1.Shape = OctEyeM2.Shape = OctEye;
    OctMouthM.Shape = OctMouth;
    int o;
    for (o = 0; o < 8; o++)
    {
        OctLegM[o] = new fk_Model();
        OctLegM[o].Shape = OctLeg;
    }
    
    OctHeadM.GlMoveTo(0, 15, 0);
    OctEyeM1.GlMoveTo(2, 14, 4);
    OctEyeM2.GlMoveTo(-2, 14, 4);
    OctMouthM.GlMoveTo(0, 13, 6);
    OctLegM[0].GlMoveTo(0, 8, 2);
    OctLegM[1].GlMoveTo(1.5, 8, 1);
    OctLegM[2].GlMoveTo(3, 8, 0);
    OctLegM[3].GlMoveTo(1.5, 8, -1);
    OctLegM[4].GlMoveTo(0, 8, -2);
    OctLegM[5].GlMoveTo(-1.5, 8, -1);
    OctLegM[6].GlMoveTo(-3, 8, 0);
    OctLegM[7].GlMoveTo(-1.5, 8, 1);
    
    OctLegM[0].LoAngle(0, -Math.PI / 20, 0);
    OctLegM[1].LoAngle(0, -Math.PI / 40, -Math.PI / 50);
    OctLegM[2].LoAngle(0, 0, -Math.PI / 20);
    OctLegM[3].LoAngle(0, Math.PI / 40, -Math.PI / 50);
    OctLegM[4].LoAngle(0, Math.PI / 20, 0);
    OctLegM[5].LoAngle(0, Math.PI / 40, Math.PI / 50);
    OctLegM[6].LoAngle(0, 0, Math.PI / 20);
    OctLegM[7].LoAngle(0, -Math.PI / 40, Math.PI / 50);
    
    var OctHeadColor = new fk_Material();
    OctHeadColor.Ambient = OctHeadColor.Diffuse = new fk_Color(0.7, 0, 0);
    OctHeadM.Material = OctMouthM.Material = OctHeadColor;
    
    var OctMouthColor = new fk_Material();
    OctMouthColor.Ambient = OctMouthColor.Diffuse = new fk_Color(0.65, 0, 0);
    OctMouthM.Material = OctMouthColor;
    for (o = 0; o < 8; o++)
    {
        OctLegM[o].Material = OctMouthColor;
    }

    OctEyeM1.SetParent(OctHeadM, true);
    OctEyeM2.SetParent(OctHeadM, true);
    OctMouthM.SetParent(OctHeadM, true);
    for (o = 0; o < 8; o++)
    {
        OctLegM[o].SetParent(OctHeadM, true);
    }

    // クマノミのモデル
    var NemoHeadM = new fk_Model();
    var NemoBodyM1 = new fk_Model();
    var NemoBodyM2 = new fk_Model();
    var NemoBodyM3 = new fk_Model();
    var NemoBodyM4 = new fk_Model();
    var NemoBodyM5 = new fk_Model();
    var NemoTailM = new fk_Model();
    var NemoUpFinM1 = new fk_Model();
    var NemoUpFinM2 = new fk_Model();
    var NemoDownFinM1 = new fk_Model();
    var NemoDownFinM2 = new fk_Model();
    var NemoEyeM1 = new fk_Model();
    var NemoEyeM2 = new fk_Model();
    
    var NemoHead = new fk_Block(1.7, 0.7, 0.9);
    var NemoBody1 = new fk_Block(2.3, 0.5, 1);
    var NemoBody2 = new fk_Block(2.8, 1, 1.1);
    var NemoBody3 = new fk_Block(2.7, 0.8, 1);
    var NemoBody4 = new fk_Block(2.3, 1, 0.9);
    var NemoBody5 = new fk_Block(1.4, 0.7, 0.8);
    var NemoTail = new fk_Prism(3, 1.5, 1.5, 0.7);
    var NemoFin = new fk_Prism(50, 0.3, 0.3, 0.5);
    var NemoEye = new fk_Sphere(100, 0.2);
    
    NemoHeadM.Shape = NemoHead;
    NemoBodyM1.Shape = NemoBody1;
    NemoBodyM2.Shape = NemoBody2;
    NemoBodyM3.Shape = NemoBody3;
    NemoBodyM4.Shape = NemoBody4;
    NemoBodyM5.Shape = NemoBody5;
    NemoTailM.Shape = NemoTail;
    NemoUpFinM1.Shape = NemoUpFinM2.Shape = NemoDownFinM1.Shape = NemoDownFinM2.Shape = NemoFin;
    NemoEyeM1.Shape = NemoEyeM2.Shape = NemoEye;    

    NemoHeadM.GlMoveTo(0, 20, 0);
    NemoBodyM1.GlMoveTo(0, 19.5, 0);
    NemoBodyM2.GlMoveTo(0, 18.8, 0);
    NemoBodyM3.GlMoveTo(0, 18, 0);
    NemoBodyM4.GlMoveTo(0, 17.5, 0);
    NemoBodyM5.GlMoveTo(0, 16.7, 0);
    NemoTailM.GlMoveTo(0, 16, -0.35);
    NemoUpFinM1.GlMoveTo(1.4, 19, 0.25);
    NemoUpFinM2.GlMoveTo(1.3, 17.3, 0.25);
    NemoDownFinM1.GlMoveTo(-1.5, 19, -0.25);
    NemoDownFinM2.GlMoveTo(-1.3, 17.3, -0.25);
    NemoEyeM1.GlMoveTo(0.2, 20, 0.5);
    NemoEyeM2.GlMoveTo(0.2, 20, -0.5);

    NemoTailM.LoAngle(0, 0, Math.PI / 2);
    NemoTailM.LoAngle(Math.PI, 0, 0);
    NemoDownFinM1.LoAngle(Math.PI, 0, 0);
    NemoDownFinM2.LoAngle(Math.PI, 0, 0);

    var NemoOrange = new fk_Material();
    NemoOrange.Ambient = NemoOrange.Diffuse = new fk_Color(0.8, 0.4, 0);
    NemoHeadM.Material = NemoBodyM2.Material = NemoBodyM4.Material = NemoTailM.Material = NemoUpFinM1.Material = NemoUpFinM2.Material = NemoDownFinM1.Material = NemoDownFinM2.Material = NemoOrange;

    var NemoWhite = new fk_Material();
    NemoWhite.Ambient = NemoWhite.Diffuse = new fk_Color(1.0, 1.0, 0.95);
    NemoBodyM1.Material = NemoBodyM3.Material = NemoBodyM5.Material = NemoWhite;

    NemoHeadM.SetParent(NemoBodyM1, true);
    NemoBodyM2.SetParent(NemoBodyM1, true);
    NemoBodyM3.SetParent(NemoBodyM1, true);
    NemoBodyM4.SetParent(NemoBodyM1, true);
    NemoBodyM5.SetParent(NemoBodyM1, true);
    NemoTailM.SetParent(NemoBodyM1, true);
    NemoUpFinM1.SetParent(NemoBodyM1, true);
    NemoUpFinM2.SetParent(NemoBodyM1, true);
    NemoDownFinM1.SetParent(NemoBodyM1, true);
    NemoDownFinM2.SetParent(NemoBodyM1, true);
    NemoEyeM1.SetParent(NemoBodyM1, true);
    NemoEyeM2.SetParent(NemoBodyM1, true);

    //細長い魚のモデル
    var LFHM = new fk_Model();
    var LFBM = new fk_Model();
    var LFTM = new fk_Model();
    var LFEM1 = new fk_Model();
    var LFEM2 = new fk_Model();

    var LFH = new fk_Prism(3, 1.2, 1.2, 1.5);
    var LFB = new fk_Block(2, 20, 1.5);
    var LFT = new fk_Cone(4, 1.2, 6);
    var LFE = new fk_Sphere(100, 0.2);

    LFHM.Shape = LFH;
    LFBM.Shape = LFB;
    LFTM.Shape = LFT;
    LFEM1.Shape = LFE;
    LFEM2.Shape = LFE;

    LFHM.GlMoveTo(0, 35.3, -0.75);
    LFBM.GlMoveTo(0, 25, 0);
    LFTM.GlMoveTo(0, 15, 0);
    LFEM1.GlMoveTo(0.3, 35, 0.8);
    LFEM2.GlMoveTo(0.2, 35, -0.8);

    LFHM.LoAngle(0, 0, Math.PI/2);
    LFHM.LoAngle(Math.PI, 0, 0);
    LFTM.LoAngle(0, -Math.PI / 2, 0);
    LFTM.LoAngle(0, 0, -Math.PI / 4);

    var LFC = new fk_Material();
    LFC.Ambient = LFC.Diffuse = new fk_Color(0.4, 0.4, 0.7);
    LFHM.Material = LFBM.Material = LFTM.Material = LFC;

    var LFEC = new fk_Material();
    LFEC.Ambient = LFEC.Diffuse = new fk_Color(0.2, 0.2, 0.2);
    LFEM1.Material = LFEM2.Material = LFEC;

    LFHM.SetParent(LFBM, true);
    LFTM.SetParent(LFBM, true);
    LFEM1.SetParent(LFBM, true);
    LFEM2.SetParent(LFBM, true);

    //サンマのモデル
    var SanmaBM1 = new fk_Model();
    var SanmaBM2 = new fk_Model();
    var SanmaTM = new fk_Model();
    var SanmaEM1 = new fk_Model();
    var SanmaEM2 = new fk_Model();

    var SanmaB1 = new fk_Block(2.5, 10, 1);
    var SanmaB2 = new fk_Block(0.5, 10, 1);
    var SanmaT = new fk_Prism(3, 2, 2, 1);
    var SanmaE = new fk_Sphere(100, 0.2);

    SanmaBM1.Shape = SanmaB1;
    SanmaBM2.Shape = SanmaB2;
    SanmaTM.Shape = SanmaT;
    SanmaEM1.Shape = SanmaEM2.Shape = SanmaE;

    SanmaBM1.GlMoveTo(0, 10, 0);
    SanmaBM2.GlMoveTo(1.5, 10, 0);
    SanmaTM.GlMoveTo(0, 4, -0.5);
    SanmaEM1.GlMoveTo(0.8, 14, 0.5);
    SanmaEM2.GlMoveTo(0.8, 14, -0.5);

    SanmaTM.LoAngle(0, 0, Math.PI/2);
    SanmaTM.LoAngle(Math.PI, 0, 0);

    var SanmaSil = new fk_Material();
    SanmaSil.Ambient = SanmaSil.Diffuse = new fk_Color(0.8, 0.8, 0.8);
    SanmaSil.Specular = SanmaSil.Emission = new fk_Color(1.0, 1.0, 1.0);
    SanmaBM1.Material = SanmaTM.Material = SanmaSil;

    var SanmaBlue = new fk_Material();
    SanmaBlue.Ambient = SanmaBlue.Diffuse = new fk_Color(0.3, 0.3, 0.6);
    SanmaBM2.Material = SanmaBlue;

    SanmaBM2.SetParent(SanmaBM1, true);
    SanmaTM.SetParent(SanmaBM1, true);
    SanmaEM1.SetParent(SanmaBM1 , true);
    SanmaEM2.SetParent(SanmaBM1, true);

    //赤・中の魚のモデル
    var RMHM = new fk_Model();
    var RMBM = new fk_Model();
    var RMTM = new fk_Model();
    var RMEM1 = new fk_Model();
    var RMEM2 = new fk_Model();
    var RMUFM = new fk_Model();
    var RMDFM = new fk_Model();

    var MH = new fk_Block(4, 2, 1.7);
    var MB = new fk_Block(5, 7, 2);
    var MT = new fk_Prism(3, 2, 2, 1.9);
    var ME = new fk_Sphere(100, 0.3);
    var MF = new fk_Block(1, 2, 1);

    RMHM.Shape = MH;
    RMBM.Shape = MB;
    RMTM.Shape = MT;
    RMEM1.Shape = RMEM2.Shape = ME;
    RMUFM.Shape = RMDFM.Shape = MF;

    RMHM.GlMoveTo(0, 20, 0);
    RMBM.GlMoveTo(0, 16, 0);
    RMTM.GlMoveTo(0, 11.5, -1);
    RMEM1.GlMoveTo(1, 20.3, 1);
    RMEM2.GlMoveTo(1, 20.3, -1);
    RMUFM.GlMoveTo(2.5, 17, 0);
    RMDFM.GlMoveTo(-2.5, 17, 0);

    RMTM.LoAngle(0, 0, Math.PI/2);
    RMTM.LoAngle(Math.PI, 0, 0);
    RMUFM.LoAngle(0, 0, -Math.PI / 5);
    RMDFM.LoAngle(0, 0, Math.PI / 5);

    var RBC = new fk_Material();
    RBC.Ambient = RBC.Diffuse = new fk_Color(0.7, 0.2, 0.2);
    RMHM.Material = RMBM.Material = RBC;

    var RFC = new fk_Material();
    RFC.Alpha = 0.1f;
    RFC.Ambient = RFC.Diffuse = new fk_Color(1.0, 0.5, 0.5);
    RMTM.Material = RMUFM.Material = RMDFM.Material = RFC;

    RMHM.SetParent(RMBM, true);
    RMTM.SetParent(RMBM, true);
    RMEM1.SetParent(RMBM, true);
    RMEM2.SetParent(RMBM, true);
    RMUFM.SetParent(RMBM, true);
    RMDFM.SetParent(RMBM, true);

    //青・中のモデル
    var BMHM = new fk_Model();
    var BMBM = new fk_Model();
    var BMTM = new fk_Model();
    var BMEM1 = new fk_Model();
    var BMEM2 = new fk_Model();
    var BMUFM = new fk_Model();
    var BMDFM = new fk_Model();

    BMHM.Shape = MH;
    BMBM.Shape = MB;
    BMTM.Shape = MT;
    BMEM1.Shape = BMEM2.Shape = ME;
    BMUFM.Shape = BMDFM.Shape = MF;
    
    BMHM.GlMoveTo(0, 20, 0);
    BMBM.GlMoveTo(0, 16, 0);
    BMTM.GlMoveTo(0, 11.5, -1);
    BMEM1.GlMoveTo(1, 20.3, 1);
    BMEM2.GlMoveTo(1, 20.3, -1);
    BMUFM.GlMoveTo(2.5, 17, 0);
    BMDFM.GlMoveTo(-2.5, 17, 0);

    BMTM.LoAngle(0, 0, Math.PI/2);
    BMTM.LoAngle(Math.PI, 0, 0);
    BMUFM.LoAngle(0, 0, -Math.PI / 5);
    BMDFM.LoAngle(0, 0, Math.PI / 5);

    var BBC = new fk_Material();
    BBC.Ambient = BBC.Diffuse = new fk_Color(0.5, 0.5, 1.0);
    BMHM.Material = BMBM.Material = BBC;

    var BFC = new fk_Material();
    BFC.Ambient = BFC.Diffuse = new fk_Color(0.8, 0.8, 1.0);
    BMTM.Material = BMUFM.Material = BMDFM.Material = BFC;

    BMHM.SetParent(BMBM, true);
    BMTM.SetParent(BMBM, true);
    BMEM1.SetParent(BMBM, true);
    BMEM2.SetParent(BMBM, true);
    BMUFM.SetParent(BMBM, true);
    BMDFM.SetParent(BMBM, true);

    //青・小のモデル
    var BSBM = new fk_Model();
    var BSTM = new fk_Model();
    var BSEM1 = new fk_Model();
    var BSEM2 = new fk_Model();

    var SB = new fk_Block(2, 4, 1);
    var ST = new fk_Prism(3, 1, 1, 0.9);
    var SE = new fk_Sphere(100, 0.2);

    BSBM.Shape = SB;
    BSTM.Shape = ST;
    BSEM1.Shape = SE;
    BSEM2.Shape = SE;

    BSBM.GlMoveTo(0, 10, 0);
    BSTM.GlMoveTo(0, 7.3, -0.5);
    BSEM1.GlMoveTo(0.5, 11.3, 0.5);
    BSEM2.GlMoveTo(0.5, 11.3, -0.5);

    BSTM.LoAngle(0, 0, Math.PI/2);
    BSTM.LoAngle(Math.PI, 0, 0);

    BSBM.Material = BBC;
    BSTM.Material = BFC;

    BSTM.SetParent(BSBM, true);
    BSEM1.SetParent(BSBM, true);
    BSEM2.SetParent(BSBM, true);

    //緑・大のモデル
    var GLHM = new fk_Model();
    var GLBM1 = new fk_Model();
    var GLBM2 = new fk_Model();
    var GLTM = new fk_Model();
    var GLEM1 = new fk_Model();
    var GLEM2 = new fk_Model();
    var GLUFM = new fk_Model();
    var GLDFM = new fk_Model();

    var LH = new fk_Block(6, 3, 3);
    var LB1 = new fk_Block(7, 10, 3);
    var LB2 = new fk_Prism(3, 4, 4, 3);
    var LT = new fk_Prism(3, 4, 4, 2.9);
    var LE = new fk_Sphere(100, 0.7);
    var LF = new fk_Block(2, 4, 1);

    GLHM.Shape = LH;
    GLBM1.Shape = LB1;
    GLBM2.Shape = LB2;
    GLTM.Shape = LT;
    GLEM1.Shape = GLEM2.Shape = LE;
    GLUFM.Shape = GLDFM.Shape = LF;

    GLHM.GlMoveTo(0, 31, 0);
    GLBM1.GlMoveTo(0, 25, 0);
    GLBM2.GlMoveTo(0, 19, 1.5);
    GLTM.GlMoveTo(0, 14, -1.45);
    GLEM1.GlMoveTo(1.7, 31, 1.5);
    GLEM2.GlMoveTo(1.7, 31, -1.5);
    GLUFM.GlMoveTo(4.2, 25, 0);
    GLDFM.GlMoveTo(-4.2, 25, 0);

    GLBM2.LoAngle(0, 0, Math.PI/2);
    GLTM.LoAngle(0, 0, Math.PI/2);
    GLTM.LoAngle(Math.PI, 0, 0);

    var GBC = new fk_Material();
    GBC.Ambient = GBC.Diffuse = new fk_Color(0.2, 0.7, 0.2);
    GLHM.Material = GLBM1.Material = GLBM2.Material = GBC;

    var GFC = new fk_Material();
    GFC.Ambient = GFC.Diffuse = new fk_Color(0.7, 0.9, 0.7);
    GLTM.Material = GLUFM.Material = GLDFM.Material = GFC;

    GLHM.SetParent(GLBM1, true);
    GLBM2.SetParent(GLBM1, true);
    GLTM.SetParent(GLBM1, true);
    GLEM1.SetParent(GLBM1, true);
    GLEM2.SetParent(GLBM1, true);
    GLUFM.SetParent(GLBM1, true);
    GLDFM.SetParent(GLBM1, true);

    //緑・小のモデル
    var GSBM = new fk_Model();
    var GSTM = new fk_Model();
    var GSEM1 = new fk_Model();
    var GSEM2 = new fk_Model();

    GSBM.Shape = SB;
    GSTM.Shape = ST;
    GSEM1.Shape = GSEM2.Shape = SE;

    GSBM.GlMoveTo(0, 10, 0);
    GSTM.GlMoveTo(0, 7.3, -0.5);
    GSEM1.GlMoveTo(0.5, 11.3, 0.5);
    GSEM2.GlMoveTo(0.5, 11.3, -0.5);

    GSTM.LoAngle(0, 0, Math.PI/2);
    GSTM.LoAngle(Math.PI, 0, 0);

    GSBM.Material = GBC;
    GSTM.Material = GFC;

    GSTM.SetParent(GSBM, true);
    GSEM1.SetParent(GSBM, true);
    GSEM2.SetParent(GSBM, true);

    //レア魚のモデル
    var GLDHM = new fk_Model();
    var GLDBM = new fk_Model();
    var GLDTM = new fk_Model();
    var GLDEM1 = new fk_Model();
    var GLDEM2 = new fk_Model();
    var GLDUFM = new fk_Model();
    var GLDDFM = new fk_Model();

    GLDHM.Shape = MH;
    GLDBM.Shape = MB;
    GLDTM.Shape = MT;
    GLDEM1.Shape = GLDEM2.Shape = ME; 
    GLDUFM.Shape = GLDDFM.Shape = MF;
    
    GLDHM.GlMoveTo(0, 20, 0);
    GLDBM.GlMoveTo(0, 16, 0);
    GLDTM.GlMoveTo(0, 11.5, -1);
    GLDEM1.GlMoveTo(1, 20.3, 1);
    GLDEM2.GlMoveTo(1, 20.3, -1);
    GLDUFM.GlMoveTo(2.5, 17, 0);
    GLDDFM.GlMoveTo(-2.5, 17, 0);

    GLDTM.LoAngle(0, 0, Math.PI/2);
    GLDTM.LoAngle(Math.PI, 0, 0);
    GLDUFM.LoAngle(0, 0, -Math.PI / 5);
    GLDDFM.LoAngle(0, 0, Math.PI / 5);

    var GLDBC = new fk_Material();
    GLDBC.Ambient = GLDBC.Diffuse = new fk_Color(0.9, 0.9, 0.9);
    GLDBC.Specular = new fk_Color(1, 1, 1);
    GLDBC.Shininess = 100f;
    GLDHM.Material = GLDBM.Material = GLDBC;

    var GLDFC = new fk_Material();
    GLDFC.Ambient = GLDFC.Diffuse = new fk_Color(0.8, 0.8, 0.8);
    GLDTM.Material = GLDUFM.Material = GLDDFM.Material = GLDFC;

    var GLDEC = new fk_Material();
    GLDEC.Ambient = GLDEC.Diffuse = new fk_Color(1, 0, 0);
    GLDEM1.Material = GLDEM2.Material = GLDEC;

    GLDHM.SetParent(GLDBM, true);
    GLDTM.SetParent(GLDBM, true);
    GLDEM1.SetParent(GLDBM, true);
    GLDEM2.SetParent(GLDBM, true);
    GLDUFM.SetParent(GLDBM, true);
    GLDDFM.SetParent(GLDBM, true);

    //ウキのモデル
    var FUM = new fk_Model();
    var FDM = new fk_Model();
    var FSM = new fk_Model();

    var FU = new fk_Sphere(100, 1);
    var FD = new fk_Sphere(100, 1);
    var FS = new fk_Cone(100, 0.2, 3);

    FUM.Shape = FU;
    FDM.Shape = FD;
    FSM.Shape = FS;

    FUM.GlMoveTo(0, 10, 0);
    FDM.GlMoveTo(0, 9.9, 0);
    FSM.GlMoveTo(0, 11, 0);

    FSM.LoAngle(0, Math.PI / 2, 0);

    var FUC = new fk_Material();
    FUC.Ambient = FUC.Diffuse = new fk_Color(1, 0, 0);
    FUM.Material = FSM.Material = FUC;

    var FDC = new fk_Material();
    FDC.Ambient = FDC.Diffuse = new fk_Color(1, 1, 0);
    FDM.Material = FDC;

    FDM.SetParent(FUM, true);
    FSM.SetParent(FUM, true);


// ウィンドウを表す変数
var window = new fk_AppWindow();
window.Size = new fk_Dimension(800, 800);
window.TrackBallMode = true;
window.Scene = SeaScene;
//window.ShowGuide(fk_Guide.AXIS_X | fk_Guide.AXIS_Y | fk_Guide.AXIS_Z | fk_Guide.GRID_XZ);

// カメラの位置と方向を設定
var SeaCamera = new fk_Model();
SeaScene.EntryModel(SeaCamera);

window.CameraModel = SeaCamera;
window.CameraPos = new fk_Vector(0.0, 20.0, 80.0);
window.CameraFocus = new fk_Vector(0, 0, -50);

//マウスポジションを3次元座標にするための変数
var outPos = new fk_Vector(0, 0, 0);
var pos = new fk_Vector(0, 0, 0);
var plane = new fk_Plane();
var planePos = new fk_Vector(0, 0, 0);
var planeNorm = new fk_Vector(0, 1, 0);
plane.SetPosNormal(planePos, planeNorm);

//モデルのエントリー
SquidHeadM.GlMoveTo(0, -10000, 0);
OctHeadM.GlMoveTo(0, -10000, 0);
NemoBodyM1.GlMoveTo(0, -10000, 0);
LFBM.GlMoveTo(0, -10000, 0);
SanmaBM1.GlMoveTo(0, -10000, 0);
RMBM.GlMoveTo(0, -10000, 0);
BMBM.GlMoveTo(0, -10000, 0);
BSBM.GlMoveTo(0, -10000, 0);
GLBM1.GlMoveTo(0, -10000, 0);
GSBM.GlMoveTo(0, -10000, 0);
GLDBM.GlMoveTo(0, -10000, 0);

window.Entry(SquidHeadM);
window.Entry(SquidBodyM);
window.Entry(SquidEyeM1);
window.Entry(SquidEyeM2);
window.Entry(SquidLegM[0]);
window.Entry(SquidLegM[1]);
window.Entry(SquidLegM[2]);
window.Entry(SquidLegM[3]);
window.Entry(SquidLegM[4]);
window.Entry(SquidLegM[5]);
window.Entry(SquidLegM[6]);
window.Entry(SquidLegM[7]);
window.Entry(SquidLegM[8]);
window.Entry(SquidLegM[9]);
window.Entry(SquidLegM[10]);
window.Entry(OctHeadM);
window.Entry(OctEyeM1);
window.Entry(OctEyeM2);
window.Entry(OctMouthM);
window.Entry(OctLegM[0]);
window.Entry(OctLegM[1]);
window.Entry(OctLegM[2]);
window.Entry(OctLegM[3]);
window.Entry(OctLegM[4]);
window.Entry(OctLegM[5]);
window.Entry(OctLegM[6]);
window.Entry(OctLegM[7]);
window.Entry(NemoHeadM);
window.Entry(NemoBodyM1);
window.Entry(NemoBodyM2);
window.Entry(NemoBodyM3);
window.Entry(NemoBodyM4);
window.Entry(NemoBodyM5);
window.Entry(NemoTailM);
window.Entry(NemoUpFinM1);
window.Entry(NemoUpFinM2);
window.Entry(NemoDownFinM1);
window.Entry(NemoDownFinM2);
window.Entry(NemoEyeM1);
window.Entry(NemoEyeM2);
window.Entry(LFHM);
window.Entry(LFBM);
window.Entry(LFTM);
window.Entry(LFEM1);
window.Entry(LFEM2);
window.Entry(SanmaBM1);
window.Entry(SanmaBM2);
window.Entry(SanmaTM);
window.Entry(SanmaEM1);
window.Entry(SanmaEM2);
window.Entry(RMHM);
window.Entry(RMBM);
window.Entry(RMTM);
window.Entry(RMEM1);
window.Entry(RMEM2);
window.Entry(RMUFM);
window.Entry(RMDFM);
window.Entry(BMHM);
window.Entry(BMBM);
window.Entry(BMTM);
window.Entry(BMEM1);
window.Entry(BMEM2);
window.Entry(BMUFM);
window.Entry(BMDFM);
window.Entry(BSBM);
window.Entry(BSTM);
window.Entry(BSEM1);
window.Entry(BSEM2);
window.Entry(GLHM);
window.Entry(GLBM1);
window.Entry(GLBM2);
window.Entry(GLTM);
window.Entry(GLEM1);
window.Entry(GLEM2);
window.Entry(GLUFM);
window.Entry(GLDFM);
window.Entry(GSBM);
window.Entry(GSTM);
window.Entry(GSEM1);
window.Entry(GSEM2);
window.Entry(GLDHM);
window.Entry(GLDBM);
window.Entry(GLDTM);
window.Entry(GLDEM1);
window.Entry(GLDEM2);
window.Entry(GLDUFM);
window.Entry(GLDDFM);

//文字列の表示
var sprite = new fk_SpriteModel();
sprite.SetPositionLT(0, 300);
sprite.Text.ForeColor = new fk_Color(0, 0, 0);
sprite.Text.BackColor = new fk_Color(1, 1, 1);
window.Entry(sprite);

// ウィンドウを開く
window.Open();

// メインループ
while (window.Update() == true)
{
    if (window.GetMouseStatus(fk_MouseButton.M1, fk_Switch.DOWN))
    {
        pos = window.MousePosition;
        window.GetProjectPosition(pos.x, pos.y, plane, outPos);
        if (outPos.x >= -250 && outPos.x <= 250 && outPos.z >= -1000 && outPos.z <= -15)
        {
            window.Entry(FUM);
            window.Entry(FDM);
            window.Entry(FSM);
            FUM.GlMoveTo(outPos.x, 0, outPos.z);
            Random r = new Random();
            int num = r.Next(1, 102);
            Random a = new Random();
            int time = a.Next(1000, 5001);

            SquidHeadM.DeleteParent(true);
            OctHeadM.DeleteParent(true);
            NemoBodyM1.DeleteParent(true);
            LFBM.DeleteParent(true);
            SanmaBM1.DeleteParent(true);
            RMBM.DeleteParent(true);
            BMBM.DeleteParent(true);
            BSBM.DeleteParent(true);
            GLBM1.DeleteParent(true);
            GSBM.DeleteParent(true);
            GLDBM.DeleteParent(true);

            Timer timer = new Timer(state =>
            {
                sprite.DrawText("HIT!!");
                if(num >= 0 && num <= 10)
                {
                    SquidHeadM.GlMoveTo(outPos.x, -3.5, outPos.z);
                    SquidHeadM.SetParent(FUM, true);
                }
                else if(num >=11 && num <= 20)
                {
                    OctHeadM.GlMoveTo(outPos.x, -6.5, outPos.z);
                    OctHeadM.SetParent(FUM, true);
                }
                else if(num >= 21 && num <= 30)
                {
                    NemoBodyM1.GlMoveTo(outPos.x, -1.8, outPos.z);
                    NemoBodyM1.SetParent(FUM, true);
                }
                else if(num >= 31 &&  num <= 40)
                {
                    LFBM.GlMoveTo(outPos.x, -12, outPos.z);
                    LFBM.SetParent(FUM, true);
                }
                else if(num >= 41 &&  num <= 50)
                {
                    SanmaBM1.GlMoveTo(outPos.x, -6, outPos.z);
                    SanmaBM1.SetParent(FUM, true);
                }
                else if(num >= 51 && num <= 60)
                {
                    RMBM.GlMoveTo(outPos.x, -6, outPos.z);
                    RMBM.SetParent(FUM, true);
                }
                else if(num >= 61 && num <= 70)
                {
                    BMBM.GlMoveTo(outPos.x, -6, outPos.z);
                    BMBM.SetParent(FUM, true);
                }
                else if(num >= 71 && num <= 80)
                {
                    BSBM.GlMoveTo(outPos.x, -3, outPos.z);
                    BSBM.SetParent(FUM, true);
                }
                else if(num >= 81 && num <= 90)
                {
                    GLBM1.GlMoveTo(outPos.x, -9, outPos.z);
                    GLBM1.SetParent(FUM, true);
                }
                else if(num >= 91 && num <= 100)
                {
                    GSBM.GlMoveTo(outPos.x, -3, outPos.z);
                    GSBM.SetParent(FUM, true);
                }
                else if(num == 101)
                {
                    GLDBM.GlMoveTo(outPos.x, -6, outPos.z);
                    GLDBM.SetParent(FUM, true);
                }
            }, null, time, Timeout.Infinite);
        }  
    }
    if(window.GetKeyStatus(fk_Key.SPACE, fk_Switch.PRESS))
    {
        FUM.LoTranslate(0, 1, 0);
        sprite.ClearText();
    }
}