using Csharp.Solutions;

namespace Inputs;

public class Input21 : AbstractInput<List<YellingMonkey>, List<YellingMonkey>>
{
    protected override List<YellingMonkey> ParseInput(string input) => input.Split('\n').Select(ParseMonkey).ToList();

    private static YellingMonkey ParseMonkey(string row)
    {
        var parts = row.Split(": ");
        if (long.TryParse(parts[1], out var shouting))
        {
            return new YellingMonkey
            {
                Name = parts[0],
                Operation = MonkeyOperation.Shout,
                YellingNumber = shouting
            };
        }

        var otherParts = parts[1].Split(' ');
        return new YellingMonkey
        {
            Name = parts[0],
            LeftOperand = otherParts[0],
            Operation = ParseOperation(otherParts[1]),
            RightOperand = otherParts[2]
        };
    }

    private static MonkeyOperation ParseOperation(string operation) =>
        operation switch
        {
            "+" => MonkeyOperation.Add,
            "-" => MonkeyOperation.Subtract,
            "*" => MonkeyOperation.Multiply,
            "/" => MonkeyOperation.Divide
        };

    protected override List<YellingMonkey> ParseInputTwo(string input) => ParseInput(input);

    protected override long SolveFirstPuzzle(List<YellingMonkey> input) => Day21.FirstPuzzle(input);

    protected override long SolveSecondPuzzle(List<YellingMonkey> input) => Day21.SecondPuzzle(input);

    public override string TestInput =>
        "root: pppw + sjmn\ndbpl: 5\ncczh: sllz + lgvd\nzczc: 2\nptdq: humn - dvpt\ndvpt: 3\nlfqf: 4\nhumn: 5\nljgn: 2\nsjmn: drzm * dbpl\nsllz: 4\npppw: cczh / lfqf\nlgvd: ljgn * ptdq\ndrzm: hmdt - zczc\nhmdt: 32";

    public override string RealInput => @"jtrg: gcpq - fvjz
vjhv: 4
fcmd: 5
djgl: gwjr - vbpp
snzw: 2
lswd: bcnm * vdhw
lqvp: 5
qwqj: 3
zrrv: 4
hpnm: 5
wsfb: pmhj + ftds
wpqh: 2
mjhb: drbf - lhtt
vmnt: 4
tqlp: 1
jddv: mllh + zwmz
lnqh: mwzc + fdwf
pmnp: dnhl * cjbb
nznb: 10
pbzf: 2
mwsh: dhff * ddzt
znwg: fvtf * pvfj
nnbp: mbzc * smhz
hlbq: 4
fqcp: 11
jzhn: 2
jlbj: dnth + jjlw
nhdh: wdhl - zsgd
qvmh: 13
ffng: 3
jsld: 2
cshh: nrdq + sbsn
jrcq: 2
bsmt: 4
wfhb: 10
cmzv: 3
plrf: 4
mmtl: 1
pqlw: 5
tstp: 4
vgjv: 6
rncj: ctft + lslz
stfq: rnjg * vqsf
rcnd: djgl * qmfb
sntt: jhrb / hqcg
fbdq: 2
stcn: tjjp + bdgs
lmcz: wmfn + gmhc
zfwp: dcst + dmlt
cssc: 1
cwfs: 2
qcts: nswn + gjgd
nwsf: 1
dmwq: 13
mdfc: 2
qswf: 2
jzjq: bfdb * qssl
cntb: 4
lsgz: 5
gnfh: fbqz * zptq
zpcm: brlb + bsmt
wjdr: 3
grfq: 5
fbqz: 2
nswn: sqlp / jctd
rszm: 2
dzpm: gmzt * nbsz
qvbz: 1
lclw: zglp * gwdq
fqhc: rncj * hfvv
wzhg: 2
fvzr: nrvw + gnfh
bznd: 7
qvgj: 11
qqjs: wrnn * vprv
swmz: 5
vnsd: 16
htdp: hcwn * vfmg
schl: sprf - bqbp
qgrh: 2
mmlz: 5
qhvj: 4
smvn: 2
cjcv: hvdv * dnfb
hgdg: sqbl / lvsn
lbbz: gdhl - dvhb
tnwh: znff / cppg
cctm: hnbf + rrzz
mtlr: 4
fnnb: wpbs + vntw
ffdv: 2
zcjd: 4
zhzr: 2
hcwn: tqcc / jlwh
zbsr: 3
fncf: 4
wdvc: 2
nstj: gglr * ffvv
dvbq: qlvv / vnhv
lpss: 4
pnlj: 5
fjwm: pljt + wsrf
gwgw: 2
ndfw: dppd + wssb
tqql: 6
prsm: trpd + rcnn
hltd: mdcm + jpjh
lmgz: wzhg * bchn
ptws: lfvh + gflw
pwjw: nvsc * gjrw
qqzg: 2
btpq: wscz + drtm
lbfr: hfqg / lzhw
jzjc: 4
gpsw: jszl * fcbh
pjzl: 5
lshr: 5
dtww: jvqv * blhs
gvsq: gdds * nvgv
ttwt: 7
frng: jrhh * bwdw
qdtc: qqnz * jwmz
smws: btmm + svdl
dwcm: hpnm * jdth
zbwr: fnhz * lvhl
crjp: zcgw * hnzn
pdfd: fhts * ddwq
tgvm: rsmh / ffpn
jtrd: twrw * nhgm
fhrq: 15
hqbg: hhwc * vsmf
wssb: rtgm + tgvj
dhff: mjft * ddwd
zczs: 3
jswv: fhls * clvh
cqdv: nfrj * pznq
blrq: 3
qrgm: rgsj - zgpd
drlt: 10
tnbr: bfwf + qltm
cvtg: 5
qzbq: hqrp + psbt
hncw: 3
mzsm: zqnb - tqcj
gbgs: vrfn * lcwj
lhcz: njrl * rztm
gsrm: chbh * dgdl
pwzq: fpdt * rznb
mbqh: tbrt * blrq
fnbb: tcdt + wjtw
gwdv: 8
fhdf: 2
sqrc: lhcz - vqnp
rrsb: jzhn * jgqm
wcwq: vmtp - wwzs
hgvf: ndws + dtdv
htvl: 7
bssv: 3
csdm: 4
zqgl: 3
tcgc: 3
gqvf: gzdz * gtss
stjc: 3
vnmt: hvbs - tvtw
cszm: bvmr * hrsr
hglr: 1
zbnp: 4
qtwt: 4
thfd: qwjw + lpvz
wrwh: dwzn / vcms
wdwf: 4
swfz: nvsl + jqrs
bwdw: msdh / nvtf
qrqq: 19
pzhz: gjgl + gvjb
nztv: 2
lcws: 2
mfmn: 3
cqwh: blbl + jplh
ltcd: pshp * pnlj
dlbb: 5
bshq: 8
tjpt: srpd * srrg
hqcg: 2
thgf: fssb / zfnh
zfbv: 2
http: vssf - nbcv
fhcq: rplb - sfnm
pvcr: 4
hzvl: 5
flzl: 2
cqpp: rmmj * pmdg
cgqp: 2
fqbg: cmzv * ncjl
tshv: flbg * vlgh
qwrp: lqhf * lttb
bvmr: 2
ppnj: 16
jsrq: 3
qnbf: jrcq * mmgn
hgnn: 2
jtgb: 5
hmtz: ffdv * vwng
lnjz: dfbw + nhjb
vmbv: lnpp * cmgr
jvgr: mrzm + jjmh
gtss: 3
fpcp: lczq / zgrr
jlfc: lstv * vfgh
wqjd: lprt + jrdc
gvsb: tpch + jhrw
lpfq: wzgj + bplr
mzhb: 7
nwcb: 2
lszb: 4
wgzs: 5
gthh: dsbq + nbgd
jrvt: 2
zmnt: 2
rczp: mqjn * lbmp
cpmj: 11
gdsh: rqdt + wmbb
gcvh: ndtg * tllz
jbrw: fgtb + hsnt
dzwf: 6
tqlq: 1
bplh: 2
fmmv: 13
lhmd: 2
qwqh: 2
qdwc: fhdf * jcvn
btmm: trnr + nljg
pzqc: 4
wrqm: 3
szct: ffsw + nwzz
gpjb: 5
htcs: lnbn - qwbp
zwsn: 2
jcmn: pvrq + jtmh
qzmg: cwqt - hhlw
nmzp: 4
ffhm: tgvm * rnrq
mcrc: 3
ndrn: 2
rggc: tgcr + wfwp
bsmg: gjjq / fmnl
grwz: bfch * mfvl
cbnr: plls - nclc
ddlg: dmtw + sjnq
jfqf: pcbc * fnnb
hwlp: fzrg * bmbg
sbsn: vdct + bdss
nprn: rbwt + snss
spjm: pppj * pbzf
trpd: 18
phrl: 5
njfq: 5
dzqb: sqzm * rhjz
bldh: snqq * tlvz
djcd: vsqb * ddvz
lqtl: cqdq - bftl
bhcq: rzwg * gvjp
bgsg: 10
qprs: 1
plls: 10
bjmj: 2
tfbz: 2
fzdd: 14
hqhc: pfsm * jfct
rdlc: 2
zfnh: 2
pwcm: wqws + qphw
qrjm: bpjc * qcrz
mnzp: jwvj + wgzs
zghq: pljc + vprr
dnfb: zdwz * qfmz
znth: 3
rlwz: 2
ggbr: 16
hrdf: 5
pghp: 1
rqvf: 3
jbmb: 14
tjbr: pmwb * nshg
hsfj: 2
ljzn: hsct + ctnr
rgtm: 5
nlwj: zllm * rzqg
swsq: sghd * nbcm
bdrv: 18
whpf: 14
fnnh: 5
bsfs: 4
rtlz: dddq + jmpb
nhgm: 5
ndws: 1
nqlc: 7
dsvw: hjfc + zhnq
fngh: 5
rljz: 2
ddzs: 2
ghfp: zfbv * lwzr
hqrp: 2
lbrp: qjfv * plsf
qlft: 4
fbrc: 18
ddwq: lclw - hvqg
pbtv: jsrg + wwpz
fgbd: hjjh * fdhf
tmqt: gvsb + dnnl
mvfn: ggbr + thqt
gmhc: 19
tglv: zsrc * sdgj
hwdn: 3
pdpz: lzvh + nvfn
blhs: jbdm / qvqb
ztvc: 7
zpjn: nsmz * smvn
sjpc: 2
gflw: zqmw + fgqc
tdvj: 2
jhhj: 5
plmg: bgsg + bnfs
vzvj: 3
rplb: vnff / drlr
mvfr: phqs + rggc
jrdc: 2
vldl: glww + tlfz
jmdl: 3
nvnp: 4
swgs: 5
pmwb: qlvb + wzzz
rhqq: 2
fgqc: dncf + tqsn
bcbr: 5
mwtd: 8
nprt: 2
ljzs: 6
wnnp: 2
czdw: fvhf * btpl
tlhj: 11
rqff: 3
vhrw: dwvz * trjz
jsft: gccz * prvd
zjzm: wzsf / bpll
dwwj: qnww / mmcf
vgqz: jcmn + mrqz
pchw: ghmt * znql
jrqb: gqzb + ndhv
dgdl: tstw + zvfn
pcbc: 3
vqnp: fqbg * nqtd
qmdd: 2
wctc: 2
cpmt: 2
tgdp: mfvn * rvhr
cdnp: vwnl + bhfp
qqpp: rtlz * fhgh
dsrp: nrwz * fzpf
mndv: 2
rgjj: lmgz - gggw
tbbv: lntf / hzbf
lnbn: flvv * ddlg
qfmz: jswv + qbjn
zclw: 4
qhtd: pwrp * hqwh
jzcz: 2
qsgm: qthv * nmzp
lzvq: nfpz + jddv
bnjm: hfrm + hwlp
njvp: 3
cctv: njhw * jcbs
lfgr: tjpz + tbhc
cmgr: 3
lrpg: jhsm * vlzc
nsvp: 4
gdhl: nbpt * rclq
hwzd: ndsq + lsmv
gsfg: 5
hfqg: mbqh + mbhw
ttbz: 2
mjbw: 7
svdn: bczr * mpmg
vhdl: mmzz - whqt
bgrr: zbnf + snqg
nwmc: mfjn + spjm
cfhf: 3
lrwn: 4
qvrz: 6
tjhv: 7
lhww: 5
htcc: nwrh / htmh
qjdt: sdgq + humn
jcll: rhqq * bfct
rzgp: ggsz * zcrr
nbgd: vvsd + gcll
jlwh: 2
vwwh: 1
czdb: 2
gbbs: 3
flfj: 3
rttt: dfmf * gcrf
twqq: dsvd * vzrl
stgf: 3
frpq: 5
gwhh: 2
mrzm: jbln + ljzn
smbv: 16
zglp: vcwt + mmhz
nzlm: 4
mbhw: jbrw + jvgr
vlzc: wbtj + sqhb
bvwd: 3
qgsf: trwl * qvrt
brlb: qsfp * sqhd
smch: 3
fgvp: 12
sbdv: 2
hcdp: tlgh * jppq
lszn: 9
dqtl: pnwd + zzss
fmwj: jtqt - gfpq
wcfl: 4
tdmq: 2
vqvv: 2
jqdf: 1
vffn: 5
hbhj: zlwm * jdqp
mwzc: 19
gbsz: npcg * cvtg
whzh: 2
rhjz: 7
lfwn: 5
jtzh: 1
hbbj: rqcn * pdwm
djlq: ncrq - ccpq
zqbp: wmvj + pdpf
bwnt: 3
ntrn: 6
nfmg: vblt * tgdp
ccvm: 20
fdhf: 2
hqjt: fwsq - pmrv
bmwj: 2
svvr: 2
jzmc: 3
jrrn: tfjh * hwnm
msmz: pdcq + ffhm
blth: 3
dmgj: bwdq * cctw
njng: 3
bpjc: 13
ztfc: fwqn + hqww
fsgw: gvgr - mdfc
lgpd: 1
jmlj: 3
zbvf: ffbn * qmvv
njjd: gcgw * lqpg
gplr: ngdz * vffp
pgrm: 18
mjqb: 5
cmtl: qsqt * wdvc
bqvs: 2
lllf: 2
pznq: 3
wbpz: cghq * tglv
fbmm: lpgn + pjrs
pljc: rcwv / vnmz
cmgq: bjcj + jzbn
fmvc: 2
rstf: 20
zlwm: 5
gzmh: lswd + zrvt
btpr: 3
tpfh: 5
vfsz: 18
qwjw: 5
hnzn: mmtl + tjtv
rgcg: 8
jdbg: 3
tvng: 2
nvbz: 2
rzwg: 5
bftl: 4
dnnl: jvjc + fnzz
jdth: 11
qhdq: 2
hpdp: phrl * nptm
jndt: 1
wsbm: spzh + hstn
shfg: zgrn * qcwb
zthv: 4
qjvv: lmgg + dgjp
tlwh: 2
qzfr: 1
jbhd: frdt + ppnj
jhsm: 11
bgwg: lwcv * bqlz
qfth: 3
wzpd: mgqp * hvns
htzw: 2
mbbn: bvwd * htvl
dppd: nptj * bwmz
qhdd: 5
dbrb: dhwd * qsjh
vbhd: wpgg * jrtg
mccw: czdb * fvzq
ljnc: 2
dcst: vvcs * fgqh
qqnz: qhgw - djct
nvsc: qwrp - wvnh
cbhp: 5
tqsz: zbnp * tpwq
cmgf: 2
lcwj: hzth + spzs
zmwj: pwlh * tfjl
sfnm: bzrh * bgns
sdfm: 2
qcrz: 2
mjcw: 9
dvdh: ctts * qcts
rfql: vpjv / mzjh
bnbz: ggmn + pghp
mhmd: 2
bplr: 5
jhcs: 2
fhls: 2
wcwg: 2
jngd: 3
twvm: 7
mfvl: vncn * zccc
sgcz: hrgv * tvdc
jjcl: wjdr * zbqp
fnzz: 6
tjjp: tnch + gqvf
qdwh: jchm * ndrn
gtbb: cqcw + tdrm
bdgs: tmfw * wbqr
swjl: ghnb + pbtv
dsqj: vlsh + hmvc
pvfj: 7
snrn: 19
vnzw: lcgc + tjpt
bdsm: 5
zbhr: zggg - vfbj
mvhp: svjl * bfgq
vfgh: gvmv / cccv
psqm: 1
jtvb: cmtl + sdfm
qdsd: tsmb * lrps
jzdl: 11
zbnf: lzsb / bshq
rwsz: flrm * htnj
wddq: qpzz * jngw
nczg: rvgj + ztjj
bzrh: 16
qpqn: cfsr * qbqq
lwzr: jtrd - qhsf
cfsr: 7
vzvr: nrzg * qzbq
zrgn: rjnp + jrqb
tgml: 1
zjzn: 4
rznb: 3
nzsw: 3
pgqj: 3
vmss: 2
mhgw: 13
rtnd: 4
jthr: 2
tstw: 12
qvcb: 4
pbhl: psmj * mbbp
jcvg: mbfh + svjg
prqt: rcnd / ncnr
rgsj: 17
nlrv: zbsr * mdhh
chdg: 5
mcbm: 6
ntps: 3
dfdj: wcnm + qvlz
zmvl: 3
zgbr: gcqd * lbrp
trjc: 20
nfrq: wcwg * bssv
qtjq: 2
qnww: vfqv + rgnc
vbsh: 3
jwvj: 2
qpzz: 2
gdjt: dqrj * lwgv
lzsg: 3
drww: 11
rlln: 2
frmj: qdwc + bldh
bfgq: ztpf + hwfm
pnwd: mndv * jchz
dptr: ttgv * jtst
nzmf: bvhz * vlvl
czjc: 2
tbcs: lggq * vmlh
sstp: qrjm * vfdb
cmqq: 2
nbzg: 6
cpfr: gzmh + chct
lhrp: qfvw * snzw
nnlw: 4
mvls: 3
ghrg: qpqn / cmpg
tjpz: hqwt * wsbm
psgm: jjcl + llsh
czfb: 3
wfwp: mmlz + lrjp
dgdf: sstp + nvnm
tttc: 7
vdct: 7
mstt: fwvh * cptm
rlzt: wjgb * tsrb
rlbv: 10
mjnb: crnp + zvlq
qlgr: 11
mjnr: 3
rtzs: 4
lhlv: fqww * rldr
ptmr: 2
nglm: 2
mtdz: njjd * gfbg
wfpc: gsgr * zdzt
jtnd: 5
gvzz: 10
lqhf: 19
rbrh: 4
qhgz: phgs * lnqg
wzjl: 12
svdl: ztzb * fmwj
vmmw: 2
ttmm: 2
vvzh: dlbn + dwcm
qfbb: 3
lntf: lnts / hfff
hwnh: 2
rtgm: 17
zrsg: 3
zzss: vmnt + zqgl
thht: 10
tvdp: trnv * zfvm
bhpv: 2
tshn: qdwh + djcd
thhg: 9
whqt: fqcp * vvzh
ttml: 9
mcsn: 16
prvw: 3
nrzm: 14
mqmf: 3
qftq: 2
pshp: 5
sqqh: 12
whcc: 7
cjqf: mjfd * qlwh
jchz: 5
dggs: 4
swgn: dlww / tvrv
zvvp: 3
gtmc: qtwt + wscl
msmh: 3
tgvj: twrz + trjc
ptrf: mtlh + ttwt
zmfw: 2
mzhg: 3
chgl: 2
rcwv: http + trqf
tbgz: zclw * hcdp
svbc: dzsb * vcbb
sltd: 16
nhjb: wlch * zbns
mbhg: prbw * htzw
bqfj: rmlg + tqpm
jswp: 10
wndm: lqfp + qrzq
qszr: lwbr * mzsm
gwjr: mwtd + nhdb
dddq: bpdt * dqtl
gqst: 2
mhbp: 3
vmgn: cnhm * vzvj
mwtb: lqtl + pzml
djnb: 3
mgwj: zfwc * vbzp
qvlz: cbrz / swgs
dmlt: tvng * pvjr
vljz: vjwv + zsth
ppjm: jmdf / qphn
ggsz: 3
fllf: vtns + rrsb
fbct: 3
rcnn: 1
frch: gpsw + blpf
nshg: 3
ghpd: 8
dwcv: 3
hhwc: 2
btvv: dpgr * hgvf
ftfn: dfzf + vcsb
mtvr: 1
bjvn: vsbt + tqsl
wbtw: dhbb + msmz
fbpq: vgrh + dqwz
rspd: dfdj + snjb
bdmd: cnrb * pcjl
zjmz: zflw + npvm
qjss: 2
mfjn: 3
bpdt: 2
rgnc: 4
tlgh: 3
wvnh: wljd + cntb
jftd: qwqh * mmtv
svtt: bcbr * hrdf
zcsq: 2
cmpb: 2
lsmv: fldl * jwnv
cfbh: 4
fnzp: hwdq + gqrz
jrhz: wshh * bqwf
cwff: fpcg / vcdp
tqsn: jsss - jndt
wsnp: 10
mbfh: 10
nnzh: 2
vvpr: mmdc + qdtc
wnbs: 2
jwgn: 5
cdhw: tbvt * cmqq
hrgv: jjqs - ghfp
gggw: 1
zbmw: nlmg + npbq
hzth: 5
sfpd: jrhz / nqgr
pbbm: 2
pqjr: vjhv * ngln
dvhb: pptt * jcvg
hbrf: rznw + brph
zbqp: 5
jrfd: 2
ttzm: 4
tzbd: 5
qthh: 12
zbzs: 4
qhdn: fgss - glfv
qcrr: cpjl + slnc
frdt: wwbb + zwsh
hzjl: qtct * wmtz
vdbw: hltd * rjlb
snqq: thfd + cwjn
nvtf: zlgj * jjfq
rldr: npcc + rjrg
pjlr: whvr + vmdp
qlvv: qlth + wtfl
mdhq: tlbj + tbgz
drpf: 2
dsvd: 2
mmgn: vtqs + wbjg
fwmd: 13
rfnb: 4
ftjn: 2
ttrt: 1
tzns: cnml * fwbj
sszm: 2
jmmd: mlns * ggsd
jngw: 3
ghdl: qlhz * wcsm
rmfn: vmhp + brbj
tdrm: jrlv * zjjb
vvsd: hdnj * jrdn
pptt: 14
wbqh: drzr - nnbp
dvrq: mbsq + hjnp
hmvc: jbnl + lzvq
djct: dzwf * dlnv
rcdr: 4
glhf: 2
nclj: 1
qlth: mzhg * pnzd
gzhh: 7
jvcd: 2
mzjh: 2
psjs: 4
rtmp: lllf * zsfr
tfqd: vqmz / tdmq
wbjg: fqhc + dnwp
hgcs: fzhc + fmdt
pnrf: 4
rjnp: jgnp + mcdl
pmml: 14
nvst: 7
ztgn: 2
ptcd: mswn * rdlc
nwrh: vbhd + srjq
spdf: 2
szwt: 10
nlws: tftg + ldsb
lnpp: lrhs + rqzs
dwzn: vdbw * jrfd
nqgr: 3
dpqs: 8
trqs: rtgq + pgpt
hfvt: 3
rqrt: 3
fnzf: rlwz * sqdw
ccpq: 9
bpzr: 10
tllz: 2
tsrb: jhhj * bplh
rlvs: wlcs + clbs
bzhc: 6
sqhb: zzsb * qvgj
qbcp: 2
trjz: rzgp + bjtr
plnr: 8
wlrd: trhr * bzsh
rptt: 2
fwht: 2
cctw: 3
zhjr: ptjl * zgmn
gmhb: wvtt * msmh
tjlr: fhjl + wndm
vjwv: zwcn * fllz
twrz: 2
whbr: 5
wrmm: qqjs / wwfm
mgvp: fncf * rlln
tclr: rfst * wcgv
wmvj: 2
wsrf: 3
czdz: 3
djvs: qswt * cwhz
fmcj: 14
gvgr: jrsq * znjl
tcpp: qvbz + ddng
nptj: 2
vdfv: dggn * zghq
lggq: 3
vvcs: drtr + fqvf
hrsr: czjc * fzdd
spzs: dmwq + cvnp
tdwc: 3
hvsq: mhbp * hdqh
hlhc: cwbv + bcsh
ndwd: dpwm * dsrm
mwjm: 1
fhbt: vbnh * sbdv
dhbb: ttmm * svbc
jmjc: 2
lvhn: 4
cjfr: 5
fzrg: 2
lslz: sfwp * nnlw
nfmq: wnbs * mfqd
qwzc: 6
fnjc: fmgv * shfg
wjpl: hbbj + wnnp
bjtr: 1
cbts: 6
pzml: gqqs * vrds
cprd: cvsc * gzrf
zrpj: swsq / jncf
rcjj: tbbv * bvhw
bwdq: 3
dfmf: ljzs + wpqh
cpjl: 5
hgbc: jhpl + fhbh
qswt: 9
tbvd: 3
smht: nzsw * ntgm
wfvc: 1
fwqn: gjnn + mrhn
rjsh: zljs / gjbl
ljnh: jshp + mdqz
qlvb: rqff * wctc
zzdc: gvhl + dsrp
pbsz: gbbs + zjzn
hzbf: 2
rbgn: 6
vvpd: vghv * jsld
bmbg: 4
gjrw: 5
fqrw: 16
mdfm: 3
zdzt: 3
jpll: fvzr * vmcp
cnrb: 2
vcwt: bslp * qjss
sgzq: ptcd + vsch
wscc: 7
nbqw: qhfc + pfft
zftd: ztnz + lshr
lwbr: 2
mjqh: mwjd + tpfh
vntw: hvsq * bmbp
jmdf: wsfb + dhvj
mlns: cgsl / cwfs
qbqq: 2
sghd: 2
wlsv: 5
mrqz: dwcz * zqwg
pgmf: pwzq * qzmg
bqlz: rbgn + qrqq
ndml: sqqh + jbtv
lwqj: ndfc + qfsd
thzq: 7
bcsh: gwbb * zmpt
jfnd: mlzr - lcvq
nwps: mnrv + nhdh
bghq: 7
ncnr: 2
rvhr: 2
wdls: bdsm + vhpb
rpcj: 5
wzgj: thhg * qfbb
crpn: gtqg + fdrw
ptsg: pwcf * cfbh
llbs: rgns - tcmn
bcbv: 1
dwpb: 2
lqhz: plrf * zfsz
jncf: 2
jbnl: sqnc - djvs
znhf: rczp * nglm
fwwb: lhrp + schl
wpbs: 1
wrsm: hlhc * njzm
fbpg: 2
hhnt: tgpw + fnmz
fmdt: mjlm + ntrn
spvg: rghc * jrrn
wlwf: rstf + clgp
whlv: 3
ttgd: 5
ssss: 3
vdff: 5
jzdr: tdpv - vnsd
pcfz: zcjd + whcc
gprb: 3
hfcp: fhpp + qdct
zzsb: 4
frhj: vnwn * fnnh
tqpm: vvbs / tfbz
jhrv: ptzt * stgf
hzbl: 2
zgqv: mtml + wfvc
jgqm: jhrv + qhph
npcg: 3
wcnm: 10
vssf: qvbr / chgl
tgcb: mqbj * qmdd
mbbp: sqsc * gpbt
wvtt: zczs * qzjd
gnjb: lnwq * qjbw
lbmp: 2
lzhw: 2
dsrm: 18
lcmq: 2
hsct: zhww * htww
qmjd: 3
flvv: 2
jctd: 2
ffbn: 3
nbcm: cjfr * jfcn
fvgl: 4
bnfs: 1
mwrv: 2
bzpj: 3
zqhv: zghv * qmtw
jfnq: 3
vcpt: 6
fztq: 2
wfbt: 2
dfbw: bbhh + gdjt
vspm: wfpc / zsnb
nrzg: 2
tvmq: 5
lljt: nvcw * hsjl
dvjd: dzfw * wsnp
qdct: ztgn * njfq
wvrf: 6
dzsb: 2
srhh: 12
jcjv: cjcv + lzcp
zrbq: 3
hbbr: czfb * mzhb
qjhl: sljw * jpbw
qltm: rlbv * zjfb
npbq: jlmf + fhdb
spwv: 2
wvrz: rpln + fwmd
lhgf: 3
gfbg: jhrs * bsfs
lsqd: 3
srzc: fhcw * fsfp
ldmm: 10
cppg: 2
vdpj: clls * mhhg
btdb: wqhm * pgrm
lrzt: 4
lnsm: 7
sfww: 3
jjbq: 7
qfvw: zcsq * ljnh
bspf: 2
smnt: 3
jjlw: fgwm * htjn
tvhp: 3
qrrh: zzcd + gwrv
mbsq: 1
cqcw: 9
bnrc: 1
gvsn: 2
tvdc: 11
pshs: 4
lhcp: 3
cghq: wlrd - rlvs
tjjg: nfmq * mjvj
hhlw: 4
fzhc: fhvr + fnzp
zjfb: ltsl + tzbd
gbqg: 2
mmgf: nstj * gcjf
dpfg: pfsj * vwvj
bfws: stcn + nndc
qdlh: 2
fdnv: vmbv - mjnb
ddqd: 2
htms: qvjr * sglf
cnrr: 6
ptsz: 5
hsjl: 6
vvlm: 2
hrwz: 4
dspv: 3
fhbh: gplr * mdrb
zgrr: 2
fdrw: lsqd * qvcb
gbtb: 2
mszz: 5
vmdp: cncm * tlhf
bfch: 2
ztnz: 1
rsmh: gfnr + snll
zmvd: 18
ctnr: 10
hqzd: swjl - fvrn
rclq: fcmc + dvtg
bqdg: bqdt - hgnn
ntzw: wscc + nbzg
dnwp: zfsh * szbm
fhts: 11
qqgf: 3
mrnd: vwwh + cdhw
rgns: vmsg + flvh
fdwf: qdsd / lbbb
szlw: 7
pqqt: 4
dbrv: 4
zrvt: wrwh * gmhb
tljd: 2
zncn: mbdv - cwff
gnvq: qqzg * ppjm
zbst: fnzf - pqlw
jszl: 4
mmzg: 2
zfvm: 5
ptqf: 4
tzfm: 1
bldl: crjp / hlls
htnj: 19
cpwn: 9
mmtv: pstr + rlqb
chtq: 3
fhdb: 14
pmdg: 4
jbtv: fmmv - hwdn
lnwq: 7
lrps: 10
btpl: 11
lnts: nqjg + brrf
cwhz: 3
vnwn: scvz + qjwp
gglr: 3
qtgc: 5
cdrc: 4
drlr: 2
vvvf: 2
qmtw: 2
clds: 2
mcqd: nlrs / mmzg
mswn: rtzs + pjhq
jrhh: 2
lqpg: 4
thqt: 8
ddvz: rfpc + ztfc
nsdg: 4
twsf: rplv + jzdl
wscz: 3
hfvv: 2
slnc: htcc + mvsw
bpss: 16
fwsr: 3
cnml: wdhs * hjqq
nfrj: svdn + tnbr
rqzs: nvzh + mvhp
ptjl: 3
vttc: jbmb - jtzh
qpvl: ghgl + thgf
wwbb: 12
vblt: 2
mtml: 6
whgj: 5
vwng: 14
fgnt: 15
pcdg: 19
wnvd: mqlm - lvpq
pcvj: gmfw + cpwn
zjjb: 2
glbh: 11
cmmv: fhcq * ljss
rqdt: lrpr + bqdg
zvfn: rpcj * thzq
lljs: tjhd * tdvj
tvrv: 2
jfcp: 5
nqjg: hbrf * zjnr
fwnj: frng + wqzm
hpnb: 3
rvcm: 3
fhcw: 5
mvnq: 4
jtnp: 4
cnhm: clzw / fwht
zfwc: 11
zwmz: 3
snjb: 10
sqzm: 17
sgpt: 3
vgtn: wcbw * cdgm
wzsf: nwmc * zpsw
bbhh: swfz - wpmd
rrnh: pjlr * zqbp
gcgw: 2
qzmb: 3
dtmr: 3
jwlg: qcrr / rbws
zwms: jmlj * qhvj
dqrj: fjwm * ffng
mpft: lbfr - qpfn
ggqn: 6
mqlm: gwgj * qjgv
htgm: nfhs * gbtb
lltz: lrwp * tnsw
ljhw: 5
wqws: 1
fgnq: lgpd + lhvb
hpzb: ljhw * mnzp
gqlc: 4
ggbs: 5
jhrw: swmz + qhct
fscd: 13
wlcs: gsrm + wcgz
tnsw: 2
fvjz: tvdp + mjlg
jgnp: 12
tvnl: 15
tmjt: 7
cznl: 11
lfvh: pbcs / qqbp
pmhj: 14
pmvr: tfdg + mbhg
njvg: zpnj * mcjc
gzdz: jqjv - rszm
rqcn: 3
pmrv: btdb - pwcm
nfpz: qbvt * fbdq
rplv: 1
ngdz: 2
gmzt: 2
clbs: hqzd * bspf
hvqg: dsvw * smch
rlqb: 2
fzlp: 2
ztzb: 2
jqrs: scbm * vgjv
hvlz: bpss + djlq
ghmt: zpwb * njng
ztjj: 4
prvd: nzlm + fgnt
bqlg: 2
rmsf: 9
dmcc: frhj * dwcv
scbm: 11
sdcp: 12
gsff: pjgf - hjvv
ndtg: lwwp / ddft
sqsc: 2
wgqb: 1
pwrp: mtdz + qswq
jbhh: rqvb + tgpj
zsfr: grww - bhcq
cdfn: gwdv * nnbt
fqql: hncw * nnjf
tfwc: 3
gzrf: btgp / npgl
sdgq: gdqn * hpdp
dlww: fvgd + fwwb
tqsl: lflb * qrqm
cqdq: dtlh * qqgf
drtr: 3
qsfp: 3
lczq: jmcg + gvnp
lzhl: 5
mtrp: mhvr / jdmv
tfjp: 17
zgnh: bgrr * ngvn
zcgw: hrwz + plnr
qhsf: 6
gjbl: 2
zggg: rjsh / nwcb
tpch: 5
tvtw: 2
snvq: tnwh + qhjp
fllz: 11
vfqv: gnjb + hmtz
gwgj: swgn - fllf
ldsb: 4
lcjl: gsrs * ngjt
qjsb: tjlr / rfhc
sqlp: lnjz * tljd
lnwn: qqdl * lgnj
ddzt: qhdn + ztvc
njwf: pbpg * ztzt
cqtd: cpmt * mjmt
qhgw: tshn / dmgj
fzpf: wjjm + tgrt
zhnq: lljt / wvrf
qbrv: 3
dwcz: 6
fhql: 3
wwzs: 5
bczr: pdfm * dnqz
ggsd: 4
jcvn: nbqw * brnt
vpsn: 4
wwwh: cznl * frpq
qhfc: hgdg * zjbl
hfrm: 18
npvg: 6
vcbn: dpct * tpvb
njbj: hbqc / sfww
wwpz: pvcr * rdff
nsmz: mdbv + jzjc
jlnp: 11
ljss: 2
bfct: 3
pgwv: ftfn + cqpp
jpgt: 2
blbl: ssss * lspg
wtfl: fwnj + frmj
dfzf: jswh + dzjc
mggd: nzsm * ptjt
gwmv: 15
zmqm: 13
bcnm: hgpb * lrpg
brnt: 2
ngvn: spsz + wzlh
hwdq: ttml + jmcc
gqqs: bscz * wbtr
dlbn: whpf * vtjl
ztzt: pdpz + mjhb
pbjp: czfp + pqjr
mtlh: lpfq / ntpq
hzrb: zqlr - nfzj
nfzj: qsgm + mdgw
ttpt: zmvd * jblr
wtsz: 3
wjgb: tqlq + ggqn
vdhw: pdfd + jmmd
dhjz: fqql + zhzr
jzhs: qzfr + gpcq
dsbq: lhlv * mdfm
grvn: 7
rtjl: 3
cbjb: 8
wshh: dbrv + lpfh
jplh: jgmw + dphd
lzcp: bglm * fmfw
fcbh: wdsr * ljnc
nfqj: 2
ggmn: jtvb / wdwf
ccbw: jpll / dnvt
zrdc: gtng * glbh
zgzl: 3
bslp: lbqq + fmcj
vsqb: mfff * hzbl
fnmz: mgwj * zrsg
qlhz: 3
phqs: zrrv * ccvm
ghnb: mpfp + qblt
zdlz: dndl + jlnp
ndfc: 3
pbcs: zbfl * ggcg
qqzv: fmvc * zncr
ffbd: drjt - lnwn
bwpf: ghpd * sctd
dpct: tsjj + nclj
hjsn: 17
vmlh: mslh + pfvl
qdcm: 3
nthl: dggs * qjsb
crnp: rnjd * jllm
qfdf: pbhl - fnrq
jgmw: 5
rjrg: 15
tjrg: 3
dppc: 7
zhvn: lszb * drww
mmdc: slwm / fmpc
fgfh: 2
nqps: jfcp * nggv
jjfq: 4
mcjc: fpcp + wqjd
szdl: 2
fvhf: 2
drhf: 3
dhvj: tvhp * qrgm
gcll: cjnc + prqt
rmmj: 5
zqlr: nszv + grvn
jjqs: sglz * cdjn
mglc: qwww * gzww
rcvg: ttzm + cdnp
tbhc: 11
nnbt: 2
tdpv: znth * htcs
jnbq: 9
zbbs: bsmg + nbvf
vfnv: 2
gcqd: vvvf * jsrp
jpjh: hbhj * btvs
jqqg: bcbv + grwz
jblr: nsrb * rvcm
npnw: fmhw / mgqv
fssh: 2
zjzr: hjvw * lvwc
jmcg: 1
szvm: mcpz * pmqp
bdss: nlws + vjhs
wcgz: zbvf * sqpr
dtwp: vfsz + hfnn
shtd: ndfw + jzsd
tbrt: bjvn * tfwc
fssb: jwlg * jthr
nrdq: dsqj * wnps
jjmh: tgcb + dzpm
jrnd: 18
ddph: sdcp + gwgw
lpgm: tjhv + hzjl
cwjn: 4
qzjd: zbwr - ntnv
qwdl: sgcz + hgbc
qswq: 3
hqwt: 2
zgmn: fgbd + rdbg
psbt: zpcm - gbqg
vjjz: jwgd + szrj
ntgm: gtwc * nlwj
mmhz: nqps * htgm
ntcc: cvmw * rljz
jpgh: mvfr + twsf
gdmt: wtfw * jjlj
sqdw: qrcv + fzpw
hbqc: zgqv * mfmn
vrfn: 3
whvf: sffg + vcbn
zncr: lpds + wlpg
bscz: 4
nvnm: zjzm + fhbt
hntc: nczg * mmwt
vghv: gtmc + qmjd
rznw: 13
zjnr: 7
gldd: rmsf - pzmg
hjsv: 5
lbbb: 2
gvjp: 2
pcgt: wjml * mhmd
pstr: 11
rnfm: 3
bvhw: 5
vbnh: 4
dtlh: sfpd * gwhh
ffpn: pshs + rlbf
nglr: mtvr + vjjz
vmtp: 20
fzpw: tjmq * bwnt
szbm: 2
mcth: 2
csmq: mdhq + mccw
sqdr: 2
vcmp: trqs * wjhg
fsfp: wrsm + wllh
lvwc: 4
lvsn: 3
czct: bjdf * ntps
dvmt: fgnq * sdbb
dtdv: rmjr * nlpm
fhjl: bdrv + szlw
tgcr: 10
brph: mpcn + zjch
bzsh: qwdl + mwsh
gfnr: fhsh + prvl
blpf: nznb * qtgc
fldl: 4
dgjp: lvhn * cbjb
njzm: 2
fmpc: 3
pdwm: 3
vtqs: jfnq * gcgc
qjwp: nprt * jwgn
ncjl: 3
clpz: nzmf * rgjj
jljs: ntbz + hzvl
ghjw: vnzw + svsh
dnvt: 4
dmtz: 20
gsrs: spvg / jhcs
szrj: 4
ncrq: gtcb * swpl
wtmq: 17
sbjd: 2
cncm: 2
dvtg: 3
fqww: 5
nzcj: 2
tnvb: 2
fqzp: tmjn + czbg
lhtt: 4
vlsd: njgb + tcpp
zpcz: 2
qsjh: 2
bjcj: mqms / rblt
ltlr: spwv * ptrf
vmvw: 2
qthv: 3
njgb: ddph + zrdc
vqsf: tnvb + lgsp
mjfd: 7
cptm: bdmd + jvjb
cjbb: 3
qjqd: pwzw * jbhh
jstm: 3
pnvs: 9
qhrj: mznz * fsdr
rrpw: sntt + nwsf
bpll: 2
hfnn: 11
lptc: 7
fmfw: 2
djhm: 2
mvlw: pwjw + nmmt
mnrv: 5
wcbw: drfr + qjhl
cqmv: jjbq * mvhz
hvjt: mtrp * zmvl
sffg: hntc - tdwj
tsmb: jvcd * cbhp
thlt: 3
wzml: 2
bvhz: 2
phgs: 3
wnft: hpnb * hfgp
mfvn: lmhb + cctm
qvjr: 2
tnch: dpfg - ptqf
cdjn: qhdq + jbhw
jrsq: 3
vnwc: mvnq + vpbg
cmsq: 5
lmth: gtbb * ddqd
spsz: dppc * vjdp
bzmp: whgj * ghrg
mjlg: nntg + qvmh
rmjr: 3
wdsr: lmth / hllp
trwl: bbmm + cmmv
wggw: vlvv + crll
zqvb: hzhq * jhdr
fmlb: 4
fsdr: fvgl * pbjp
dmtw: 10
nndc: ttpt + lbbz
brqf: ljmq + pmnp
cvnp: jdbg * tbvd
fwvh: 3
bfdb: 11
hlls: 4
fwbj: 5
jvcb: 4
mwbv: jsft / nbmb
trqf: 12
glww: pbbm * dwwj
svhq: vltb + wzpd
hjnp: 6
pfpz: lvfr * gbpm
lfvb: 2
qhct: cmpb + hjsv
mjvj: 13
tlbj: jtgb * brqf
cgsl: hbrc * zhcn
ntcg: ldmm + tbcs
vjdp: mszz * szwt
zbns: qpvl - cjld
qwww: 20
zgpd: 4
mdcm: qhrj + vmgn
ztfz: 6
clgp: 10
zsth: vzvr * nnzl
tqcj: 3
fhpp: 1
hzfc: 5
jzsd: 4
rblt: 2
bqbp: 13
bqcm: fbpg * rfql
dpld: 2
jhdr: jzcz * cqwh
tzng: 12
shnj: hpzb * grfq
lwgv: 4
jsrg: zmfw * gjmm
zhcn: 2
lstv: 2
gcpq: zjmz / nfrq
gpbt: 9
gjgl: fwwf * prvw
ldch: mggd * whlv
lflb: 4
qvrt: 2
psmj: chjp - ptws
qblt: vhrj * lfwn
ptzt: 3
mdbv: 9
rbwt: 3
rpln: 7
rlbf: 2
fhgh: wzjl + dzqb
rjbf: 3
pwss: 3
mdzz: cgqp * svhq
rdbg: dbrb / mwrv
prbw: 3
fwwf: 2
dnqs: jpvz + qhtd
nzsr: 2
mcnw: smws / mqmf
jpvz: zpmt * tbsc
dzht: 7
lrwp: 3
gjmm: 12
cmpg: 2
zccc: 5
gjgd: bfws * ddzs
gfpq: zbmw / ncpg
mjmt: glrg * jsrq
zdwz: 2
jllm: 6
lwcv: 2
bpzz: jpts * cmsq
vftq: shnj - jrnd
vlvv: lhmd * pmml
zmpt: 4
zhww: 11
jmpb: 3
lpgn: 5
cvmw: bbnp * ptmr
njhw: 3
zpsw: 4
gvhl: hrnd + qqpp
hjqq: 2
jpdq: 3
nszv: fscd * qlft
glgs: rjtw + vmvw
wfgq: fbrc + qthh
wcdr: 2
wjhg: dgdf + hvlz
qnbb: gpsl / ngdp
qnsz: rwpv + nghz
snqg: 4
dggn: 4
sqbl: mjnr * jhvn
hsnt: rwzn + thzn
zsgd: zgzl * nfqj
cbmq: 14
gqrz: qwzc + dtwp
htmh: 4
fgtb: sbhb * ghdl
lmwt: lzhl + tpdz
tpwq: 17
zpnj: 2
vjrj: bzpj * mvls
lttb: 2
jdmv: 2
jbbm: 5
qsbt: nglr + scjp
chct: vcmp * fnjc
wtfw: 2
tgbl: 5
fgwm: 2
zjch: shtd + vdpj
qjfv: 5
btzb: 5
rnjg: 3
nlpm: 2
mpcn: sgqz * bmwj
gsgr: ntcc / vpsn
ndhv: ntzm / lfvb
cvsc: 2
thzn: tpnn - tjbr
rrzz: 9
btvh: nvst + pnrf
fhjw: tgml + btpq
gvjb: lptc * svvr
lbns: gpsn + wnft
jbdm: mcbm * fjrl
vvss: fzlp * sqrc
jqgl: 4
wbtj: glgs + bzwc
pnzd: bwpf + mqrg
qvbr: jcdv + cmqc
pfft: 3
jjlj: 5
gcrf: 8
ddft: 2
tmgs: cprd + wbpz
wpml: 3
ljmq: 14
fwtz: 3
bglm: 7
gwrv: wtsz * hdwb
lzsb: vvpd * rzrv
qlwh: 2
srrg: 4
pwzl: jzwn * pwss
zbth: 1
bmbp: 2
lflt: lrzt + btzb
trnv: zppz / vbsh
srvs: 17
ntnv: cszm * nwps
fqgf: 4
pppj: 5
wftw: qnbf + bldl
vnff: vvpr + njwf
gtng: 3
vbpp: 4
nghz: lbns * ljrs
gvgb: cbmq / wcdr
pwlh: 4
pwzw: 4
vvmp: dwpb * zbzs
wmtz: jwqz * dpld
lzvh: chtq * jpqs
fgss: nnzh * gwmv
ctts: zzdc + vljz
tsjj: 12
gcgc: 19
msdv: 3
gzww: fngh + sgpt
qssl: 2
mjgs: 10
vltb: pcdg * gprb
vlsh: 14
tcdt: jtrg / bznd
gbpm: gdmt - qzmb
tqcc: zfwp + snrn
hjjh: tmjt * mcqd
twrw: 5
znjl: 3
dndl: 2
jvqv: 18
gccz: 2
qpfn: snlj * fvgh
flrm: 2
wdhs: fbct * lhgf
nqjw: 2
zsnb: 3
cmqc: tqsz * jswp
bbhm: 18
mpfp: lnsm * hfvt
tlvz: wdls - nzcj
lnzn: 16
wqzm: ptsz * qbmq
crll: tzns / qgrh
htww: 9
dtqt: 2
jzwn: 2
hdqh: jvcb + czdz
fhvr: 15
btgp: nztv * cpfr
njrz: djtd + twvm
gwdq: 4
nbcv: mglc + dmcc
vvbs: tfqd - vnwc
vffp: 4
jtzr: 3
jqjv: njvg - drlt
tfdg: 1
jvjc: 5
sdgj: mcrc * gbgs
tcmn: 5
qfsd: 18
ffvv: 2
jrdn: brts + qbrv
mgfs: trzc + vhwv
mjlm: fgvp * vmss
mllh: nrzm * vqvv
pfsj: 3
rrfv: pgzz + psqm
fvtf: 11
mhvr: vnmt * qswf
tgrt: sltd * cqmv
wjml: tvmq + wrmm
wbtr: 3
tlhf: 3
cccv: 2
cjld: tstc + lmwt
dqwz: 5
nvgv: 13
rdff: zhjr / rjbf
cbrz: mbbn * lhww
mswc: 2
bqdt: mwjm + hqgc
tpdz: gldd + hfws
hllp: 2
tvrz: pfpz * fhrq
smhz: 3
fdcl: 5
hfws: drpf * wrmz
wcgv: 4
qmvv: wsgw * bngr
zzcd: 4
pdcq: dspv * tnhj
dzfw: 5
lsct: svlt * mtlr
rmlg: 2
znff: vqzj * pcfz
tgpj: 5
mrhn: 1
tfjh: glth + hfcp
jpqs: 5
mtbp: 3
lpzj: 3
jdgg: jtnp * sjpc
qhph: lcjl / ttbz
vbzp: 11
sqpr: wpml * cmgq
zpwb: 7
phnp: rbtv + wcfl
bchn: sgzq / nprn
dphd: 3
lffw: msdv * czrh
mmwt: 7
pfsm: 3
tbsc: jmdl + fqrw
rnrq: 4
nzsm: 3
trjj: 5
drbf: znhf * psjs
fpcg: tqql * zbhr
ggps: wbrr + fqzp
zgwl: fhql * blth
ncpg: 3
qphw: 10
mfff: 7
bqwf: 3
cwqt: dfjw + rtmp
lgnj: 7
jtqt: szvm * dvdh
wwfm: 3
pfhq: bfqr * wvff
hvdv: 2
wgmt: gvzz + nthl
lvhl: vvss / hrwc
glfv: 6
wsgw: mnqq - tqlp
nggv: 2
gpsn: jqdf + ztfz
tfjl: 4
zppz: mtbp * whvf
mrdm: jrvt * srhh
bwzr: chdg * qhdd
nlmg: qfdf * qgtn
wzlh: zqvb + rcjj
btwt: ggps + qsbt
pdpf: 9
qhjp: 1
wqdw: dnqs * tmgs
bgns: nchh * mjqh
ntpq: 2
dnqz: 2
bbmm: czln * qjvv
bhbq: mjgs * zqhv
rhjr: 2
nchh: 4
hstn: 3
hjvv: 1
dncf: 17
vncn: 7
rztm: 19
hdnj: pqqt + hqhc
ztpf: wnmv * psgm
bjdf: 5
tcqh: 2
ghgl: mhss + vdnc
pcjl: 4
prvl: tcgc + rtnd
zghv: rspd * fssh
mcpz: 3
fvgd: ddqf / mgfs
mcdl: bqfj * tcqh
wmbb: lnzn - pjzl
vtjl: 2
rjtw: 5
zpmt: jcjv + qpmr
hrnd: dptr * vlsd
nczf: 2
zfsh: pfhq - vhrw
pvjr: clpz / rptt
zptq: fbpq + wnrh
qmfb: 2
clvh: 5
vcbb: 4
hvbs: bbhm + gbsz
sglf: 11
gwbb: 2
zvmr: 18
fmnw: jfnd / bwlr
swpl: 2
tpvb: 2
fvgh: lljs / ftjn
tbvt: 11
mmzz: qjdt / lzsg
nclc: 3
qvqb: 2
mpmg: 2
wpgg: lwqj + wddq
qjbw: 2
gvnp: lffw + bqlg
cwbv: fhjw * cdbt
zwcn: rrpw * jlbj
mqjn: 3
lpfh: 15
pmqp: zgbr + zgnh
snss: 3
mdqz: rttt - phnp
hvns: 2
bwmz: 8
clls: fwsr * wrqm
fqvf: 4
tpmj: 4
mnqq: bhpv * nlrv
gjnn: 15
hgpb: dvmt + gqlc
tmjn: 4
nvfn: qftq * gvgb
wqhm: 3
mfqd: 3
rjlb: 2
rbws: 2
gdds: 2
drzr: vdff * wjpl
pwzr: 3
bngr: 2
bssz: 9
zjbl: 2
scvz: gzhh * qdcm
vhrj: blfd / nzsr
mlzr: fnbb * vldl
mslh: 17
lrhs: hhnt * qtjq
dnth: 19
hfff: 2
jcbs: 5
fcmc: tvnl + tshv
pljt: 4
shdh: 2
nvsl: mdzz + wbqh
lztr: dtqt * jlfc
lqfp: 2
nmmt: ltlr + ntcg
mdrb: qjqd / cdrc
jwgd: 3
mdhh: 2
llsh: 6
fdzt: ttgd + hglr
msdh: tmqt * flzl
nqtd: 3
dzjc: 5
wvff: 5
splz: 11
mmpn: 2
gtcb: ggbs * jzmc
qpmr: dtww + wvrz
jmcc: pchw - btvh
nnjf: 3
cdgm: 11
jlmf: 3
fmnl: 3
hvmb: 10
wghc: rgtm * jbbm
vsbt: jtnd * djnb
jcdv: wnvd * glhf
dpwm: 6
lbqq: 5
vprv: wzml + fbmm
chbh: svtt + bwzr
czln: sqrh + nvbz
cjnc: 18
plsf: lfgr + tclr
vgrh: 4
sgqz: mwbv * djhm
jvjb: lflt * qlgr
zfsz: whzh + nqlc
ltsl: 1
mvhz: 5
czfp: 1
rghc: 2
vtns: zrgn + lpfj
nptm: 4
qwbp: pbsz + rbrh
vhpb: 8
gzjw: 18
tlfz: 7
pjhq: 2
jwqz: 4
ptjt: 3
pgzz: wlwf - rgcg
zljs: wgmt + czdw
fnhz: jzhs * pnvs
fgqh: jtzr + rfnb
brts: 8
mgqv: 2
flbg: btwt + vcpt
zfwf: 2
wnmv: 3
zrwh: 10
qcwb: 2
gfwv: 2
pwcf: 2
mqhj: 3
hqwh: mvfn + lgjg
bzwc: hbbr + njbj
sbhb: 14
pwww: 4
jppq: 4
djvn: 3
jhvn: cdfn + wdlz
lmhb: lrwn * lltz
rwpv: mwtb * shdh
vnmz: 5
wbrr: 12
tjhd: qnbb + bpzr
lcgc: gplp * wbvj
mmcf: 2
gdqn: hzfc * gsff
vprr: vftq + tpmj
mhhg: jngd * jstm
lhvb: 5
vqzj: 2
fmgv: lztr + cqdv
hfgp: 2
hdwb: 11
vpbg: 7
nbpt: 10
czbg: 4
dlnv: mhgw * tlwh
jhqp: jqqg + llbs
bwlr: 3
vmhp: njvp + mrdm
qrqm: 11
hbrc: mvmf + wwwh
lgjg: fqgf + hwzd
ddng: 6
gjjq: vdfv - srzc
wnps: 2
nvcw: vfnv * lrtp
zbfl: 4
rnjd: pzhz + hvjt
hjvw: 8
nfhs: lfgh / fgfh
svlt: 7
hqww: 2
hnbf: 20
vfdb: 13
jtst: 10
drtm: rnfm * brrd
jzbn: ffbd * lpzj
fmhw: mcth * jhqp
lmgg: ndml - zbth
mqbj: gqst * qszr
tjtv: 6
wntd: vspm * gsfg
tnhj: 5
gpcq: fmlb * hlbq
vsmf: mjbw * splz
sqhd: 3
jrtg: 2
glrg: 7
vdnc: fztq * zbst
drfr: 3
lpfj: vjrj + ldch
nnzl: cpmj + ndwd
lpds: rtjl * bnbz
nvzh: npnw * vjwq
vqmz: jzdr * nczf
qqbp: 4
wmfn: hgcs / jmjc
jpts: 6
qgtn: 2
ntbz: 6
mjft: 4
dvzb: wntd - gvsq
mwjd: wfbt * qwqj
vlvl: 3
zsrc: zvmr + ccbw
root: mcnw + wqdw
trzc: 1
sprf: fcmd * jftd
ngln: 4
mgqp: 16
humn: 3803
zllm: 2
czrh: 3
btvs: gnvq + smht
ctft: 15
jrwj: 6
fpdt: 17
blsf: wfhb * lwbw
srjq: rsrw * clds
gplp: 3
nfgj: jnnn + zwms
qqdl: 2
vjwq: 3
jchm: mmgf + fmnw
wmnp: 5
njrl: 7
slwm: bhbq - vgtn
wdhl: prsm * spdf
qvvp: rcvg + tzfm
npcc: 4
ggcg: tzng + crpn
lcvq: lpgm * qrrh
zqwg: 3
svjl: 4
gtwc: 2
gmfw: 4
jhpl: rmfn + cssc
qbvt: wgqb + gdsh
zgrn: 4
ljrs: zmnt * jzjq
pvrq: fsgw + pcgt
mvsw: 3
trhr: bnjm + fwtz
ngjt: 2
hwnm: 2
clzw: hqjt + mvlw
srpd: 4
lgsp: 5
tpnn: tvrz + btvv
rzrv: 4
gcjf: 20
lrtp: mrnd + vvmp
lwwp: mmpn * jljs
nntg: 10
htjn: tdwc * djvn
sglz: flfj * zgwl
rpsz: 2
qsph: 7
ddwd: nsvp * jrwj
jwmz: wlsv * drhf
hzjp: 3
zvlq: bzmp * gfwv
wpmd: 10
bbnp: lnqh / dtmr
lfgh: zdlz + pcvj
tjmq: 3
spzh: 16
jsrp: dvbq + rvwq
nhdb: plmg + sbjd
pjgf: lpss * zwsn
hwfm: rpsz + mgvp
dvbt: rlzt / sqdr
trnr: dvzb + htdp
rtgq: pgbr + njrz
gtqg: whbr * wmnp
znql: 2
vwnl: bghq * blls
jcgv: 11
jbhw: 5
pgbr: 4
gvmv: wftw * sszm
lvfr: smnt + jqgl
lspg: wrnd / qbcp
vrds: 8
qrzq: lhcp * tgbl
rzcv: 5
wzzz: jbhd + zfwf
dpgr: 7
tncv: lnbt + qsng
zlgj: 2
dnhl: 11
vjhs: 1
hfgn: 4
rfst: 4
tftg: 3
sqrh: 5
wlpg: gpjb * hsfj
nlrs: nqjw * dzht
gqzb: 1
zqnb: gvsn * btfd
nwzz: jfqf * lcmq
wrmz: 8
cdbt: 3
zgdn: 5
hjfc: tjrg * tfjp
zflw: mstt + hqbg
pjrs: 2
ndsq: 1
jsss: 8
vnhv: 2
qbjn: mswc * qfth
rfpc: zjzr + lszn
jtmh: csdm * czct
vfmg: 2
wjjm: pgmf / pwzl
drjt: vttc + bpzz
ffsw: hzjp * zbbs
lprt: pmvr + rdpc
lvpq: szdl * cshh
qsqt: 11
fnrq: nfmg + csmq
snlj: 2
qsng: 6
bfqr: rcdr + ntzw
fvrn: qhgz + qqzv
wnrh: 7
chhz: 2
mqrg: nfgj * rqrt
vhwv: cfhf + bzhc
jshp: bssz * qvrz
svjg: fdcl + bnrc
vsch: npvg * ffdw
rvgj: 18
mznz: nsdg * bjmj
btfd: pzqc + qsph
rvwq: jdgg * lsct
hrwc: 4
wdlz: 3
ffdw: 5
brbj: tlhj + jcgv
mbzc: 2
vzrl: 4
wscl: blsf + zpjn
ddqf: qgsf - qnsz
wrnn: 2
blfd: tjjg + rwsz
tstc: hvmb + jpdq
nbsz: srvs * brbb
pwzz: 2
rzqg: 4
brbb: 7
qtlq: thht + mqhj
fhsh: 3
sqnc: dvjd + dvbt
vfbj: vvlm + tstp
svsh: bgwg - qtlq
grww: thlt * wtmq
fwsq: wggw * brgc
lrjp: 2
wbqr: 10
ttgv: 3
qtct: 2
sljw: jcll + ttrt
nrwz: 3
pzmg: 2
nbvf: wcwq * tncv
jfcn: htms / pwzz
cpzn: 3
vlgh: 2
nbmb: 2
jhrs: 2
glth: cjqf + stfq
vwvj: 7
jpbw: 4
nljg: rrnh * jpgt
brrf: gthh + bqcm
snll: zmwj + smbv
cwqj: rzcv * cnrr
wjtw: ltcd * cbnr
tmfw: 2
wcsm: rqvf * hnqr
ntzm: hfgn * zftd
jnnn: 1
jbln: vgqz + frch
lpvz: ptsg * pwww
qphn: 2
vmcp: 4
qjgv: btpr + pwzr
djtd: 6
wllh: vffn * hjsn
fvzq: rrfv + qvvp
pgpt: stjc * rhjr
jdqp: 2
whvr: zthv + pgqj
gtlc: rjzj * gzjw
jwnv: 3
qrcv: 14
rwzn: vhdl * znwg
lrpr: 3
mhss: ghjw - wghc
zqmw: 4
rbtv: dhjz + hwnh
hzhq: cqtd + dlbb
pdfm: zrpj + fbpt
jrlv: 4
zcrr: 2
mbdv: mpft * bqvs
wrnd: cmgf * dvrq
bfwf: zgdn * pgwv
blls: 2
jhrb: 20
wljd: 8
nsrb: 5
sjnq: jnbq - zpcz
nrvw: zmqm + mcsn
tgpw: zncn / mjqb
grqn: 1
brrd: 3
brgc: 2
rdpc: 2
dhwd: zhvn + cctv
sctd: 5
wlch: 2
rfhc: 2
npgl: 2
vpjv: jpgh * chhz
sdbb: 3
lwbw: 2
zwsh: 1
fbpt: 18
vcdp: 2
gpsl: lmcz * lcws
mdgw: 4
sfwp: qdlh * nvnp
jswh: 1
vcsb: 1
vcms: 4
chjp: szct / mjcw
hnqr: 3
dfjw: zrwh * fdzt
scjp: 3
dwvz: 2
dgjh: 10
mvmf: wbtw * cbts
lnqg: vmmw * hzrb
jfct: 3
ftds: grqn + gcvh
mqms: lqhz + dpqs
fjrl: 4
pbpg: 7
tdwj: wfgq - tttc
vmsg: 13
flvh: 10
qbmq: lsgz * twqq
pfvl: 14
lnbt: 15
wbvj: 9
npvm: gtlc + fdnv
rjzj: dmtz + zrbq
bhfp: qprs + dgjh
rsrw: trjj * lqvp
hqgc: cpzn * zvvp
rqvb: cwqj + snvq
ngdp: 2";
}