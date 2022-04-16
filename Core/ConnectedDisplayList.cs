using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

/*
 * 
 * 코드 참조 : https://icodebroker.tistory.com/m/9289
 * 
 */


namespace AutoDisplayRotate.Core
{
    /// <summary>
    /// 모니터 헬퍼
    /// </summary>
    public static class ConnectedDisplayList
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////// Enumeration
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region 장치 구성 질의 플래그 - QUERY_DEVICE_CONFIG_FLAG

        /// <summary>
        /// 장치 구성 질의 플래그
        /// </summary>
        public enum QUERY_DEVICE_CONFIG_FLAG : uint
        {
            /// <summary>
            /// QDC_ALL_PATHS
            /// </summary>
            QDC_ALL_PATHS = 0x00000001,

            /// <summary>
            /// QDC_ONLY_ACTIVE_PATHS
            /// </summary>
            QDC_ONLY_ACTIVE_PATHS = 0x00000002,

            /// <summary>
            /// QDC_DATABASE_CURRENT
            /// </summary>
            QDC_DATABASE_CURRENT = 0x00000004
        }

        #endregion


        #region 디스플레이 구성 / 비디오 출력 기술 - DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY

        /// <summary>
        /// 디스플레이 구성 / 비디오 출력 기술
        /// </summary>
        public enum DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY : uint
        {
            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_OTHER = 0xFFFFFFFF,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HD15 = 0,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SVIDEO = 1,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPOSITE_VIDEO = 2,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_COMPONENT_VIDEO = 3,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DVI = 4,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_HDMI = 5,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_LVDS = 6,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_D_JPN = 8,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDI = 9,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EXTERNAL = 10,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_DISPLAYPORT_EMBEDDED = 11,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EXTERNAL = 12,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_UDI_EMBEDDED = 13,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_SDTVDONGLE = 14,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_MIRACAST = 15,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_INTERNAL = 0x80000000,

            /// <summary>
            /// DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32
            /// </summary>
            DISPLAYCONFIG_OUTPUT_TECHNOLOGY_FORCE_UINT32 = 0xFFFFFFFF
        }

        #endregion


        #region 디스플레이 구성 / 모드 정보 타입 - DISPLAYCONFIG_MODE_INFO_TYPE

        /// <summary>
        /// 디스플레이 구성 / 모드 정보 타입
        /// </summary>
        public enum DISPLAYCONFIG_MODE_INFO_TYPE : uint
        {
            /// <summary>
            /// DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE
            /// </summary>
            DISPLAYCONFIG_MODE_INFO_TYPE_SOURCE = 1,

            /// <summary>
            /// DISPLAYCONFIG_MODE_INFO_TYPE_TARGET
            /// </summary>
            DISPLAYCONFIG_MODE_INFO_TYPE_TARGET = 2,

            /// <summary>
            /// DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32
            /// </summary>
            DISPLAYCONFIG_MODE_INFO_TYPE_FORCE_UINT32 = 0xFFFFFFFF
        }

        #endregion


        #region 디스플레이 구성 / 장치 정보 타입 - DISPLAYCONFIG_DEVICE_INFO_TYPE

        /// <summary>
        /// 디스플레이 구성 / 장치 정보 타입
        /// </summary>
        public enum DISPLAYCONFIG_DEVICE_INFO_TYPE : uint
        {
            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_GET_SOURCE_NAME = 1,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME = 2,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_PREFERRED_MODE = 3,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_GET_ADAPTER_NAME = 4,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_SET_TARGET_PERSISTENCE = 5,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_BASE_TYPE = 6,

            /// <summary>
            /// DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32
            /// </summary>
            DISPLAYCONFIG_DEVICE_INFO_FORCE_UINT32 = 0xFFFFFFFF
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Structure
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region LUID - LUID

        /// <summary>
        /// LUID
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 하위 파트
            /// </summary>
            public uint LowPart;

            /// <summary>
            /// 상위 파트
            /// </summary>
            public int HighPart;

            #endregion
        }

        #endregion

        #region 디스플레이 구성 / 경로 소스 정보 - DISPLAYCONFIG_PATH_SOURCE_INFO

        /// <summary>
        /// 디스플레이 구성 / 경로 소스 정보
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_SOURCE_INFO
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 어댑터 ID
            /// </summary>
            public LUID AdapterID;

            /// <summary>
            /// ID
            /// </summary>
            public uint ID;

            /// <summary>
            /// 모드 정보 인덱스
            /// </summary>
            public uint ModeInforIndex;

            /// <summary>
            /// 상태 플래그
            /// </summary>
            public uint StatusFlag;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 경로 타겟 정보 - DISPLAYCONFIG_PATH_TARGET_INFO

        /// <summary>
        /// 디스플레이 구성 / 경로 타겟 정보
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_TARGET_INFO
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 어댑터 ID
            /// </summary>
            public LUID adapterID;

            /// <summary>
            /// ID
            /// </summary>
            public uint ID;

            /// <summary>
            /// 모드 정보 인덱스
            /// </summary>
            public uint ModeInfoIndex;

            /// <summary>
            /// 출력 기술
            /// </summary>
            private DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology;

            /// <summary>
            /// 회전
            /// </summary>
            //private DISPLAYCONFIG_ROTATION Rotation;

            /// <summary>
            /// 축적
            /// </summary>
            //private DISPLAYCONFIG_SCALING Scaling;

            /// <summary>
            /// 리프레쉬 비율
            /// </summary>
            private DISPLAYCONFIG_RATIONAL RefreshRate;

            /// <summary>
            /// 스캔 라인 순서
            /// </summary>
            //private DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering;

            /// <summary>
            /// 타겟 이용 가능 여부
            /// </summary>
            public bool TargetAvailable;

            /// <summary>
            /// 상태 플래그
            /// </summary>
            public uint StatusFlag;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 해석 - DISPLAYCONFIG_RATIONAL

        /// <summary>
        /// 디스플레이 구성 / 해석
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_RATIONAL
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 분자
            /// </summary>
            public uint Numerator;

            /// <summary>
            /// 분모
            /// </summary>
            public uint Denominator;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 경로 정보 - DISPLAYCONFIG_PATH_INFO

        /// <summary>
        /// 디스플레이 구성 / 경로 정보
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_PATH_INFO
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 소스 정보
            /// </summary>
            public DISPLAYCONFIG_PATH_SOURCE_INFO SourceInfo;

            /// <summary>
            /// 타겟 정보
            /// </summary>
            public DISPLAYCONFIG_PATH_TARGET_INFO TargetInfo;

            /// <summary>
            /// 플래그
            /// </summary>
            public uint Flag;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 2D 영역 - DISPLAYCONFIG_2DREGION

        /// <summary>
        /// 디스플레이 구성 / 2D 영역
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_2DREGION
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// X 카운트
            /// </summary>
            public uint CountX;

            /// <summary>
            /// Y 카운트
            /// </summary>
            public uint CountY;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 비디오 신호 정보 - DISPLAYCONFIG_VIDEO_SIGNAL_INFO

        /// <summary>
        /// 디스플레이 구성 / 비디오 신호 정보
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_VIDEO_SIGNAL_INFO
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 픽셀 비율
            /// </summary>
            public ulong PixelRate;

            /// <summary>
            /// 수평 동기화 주파수
            /// </summary>
            public DISPLAYCONFIG_RATIONAL HorizontalSynchronizationFrequqncy;

            /// <summary>
            /// 수직 동기화 주파수
            /// </summary>
            public DISPLAYCONFIG_RATIONAL VerticalSynchronizationFrequency;

            /// <summary>
            /// 활성 크기
            /// </summary>
            public DISPLAYCONFIG_2DREGION ActiveSize;

            /// <summary>
            /// 전체 크기
            /// </summary>
            public DISPLAYCONFIG_2DREGION TotalSize;

            /// <summary>
            /// 비디오 표준
            /// </summary>
            public uint VideoStandard;

            /// <summary>
            /// 스캔 라인 순서
            /// </summary>
            //public DISPLAYCONFIG_SCANLINE_ORDERING ScanLineOrdering;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 타겟 모드 - DISPLAYCONFIG_TARGET_MODE

        /// <summary>
        /// 디스플레이 구성 / 타겟 모드
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_MODE
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 타겟 비디오 신호 정보
            /// </summary>
            public DISPLAYCONFIG_VIDEO_SIGNAL_INFO TargetVideoSignalInfo;

            #endregion
        }

        #endregion

        #region 디스플레이 구성 / 모드 정보 통합 - DISPLAYCONFIG_MODE_INFO_UNION

        /// <summary>
        /// 디스플레이 구성 / 모드 정보 통합
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        public struct DISPLAYCONFIG_MODE_INFO_UNION
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 타겟 모드
            /// </summary>
            [FieldOffset(0)]
            public DISPLAYCONFIG_TARGET_MODE TargetMode;

            /// <summary>
            /// 소스 모드
            /// </summary>
            //[FieldOffset(0)]
            //public DISPLAYCONFIG_SOURCE_MODE SourceMode;

            #endregion
        }

        #endregion


        #region 디스플레이 구성 / 모드 정보 - DISPLAYCONFIG_MODE_INFO

        /// <summary>
        /// 디스플레이 구성 / 모드 정보
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_MODE_INFO
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 정보 타입
            /// </summary>
            public DISPLAYCONFIG_MODE_INFO_TYPE InfoType;

            /// <summary>
            /// ID
            /// </summary>
            public uint ID;

            /// <summary>
            /// 어댑터 ID
            /// </summary>
            public LUID AdapterID;

            /// <summary>
            /// 모드 정보
            /// </summary>
            public DISPLAYCONFIG_MODE_INFO_UNION ModeInfo;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 타겟 장치명 플래그 - DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAG

        /// <summary>
        /// 디스플레이 구성 / 타겟 장치명 플래그
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAG
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            /// <summary>
            /// 값
            /// </summary>
            public uint Value;
        }

        #endregion
        #region 디스플레이 구성 / 장치 정보 헤더 - DISPLAYCONFIG_DEVICE_INFO_HEADER

        /// <summary>
        /// 디스플레이 구성 / 장치 정보 헤더
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISPLAYCONFIG_DEVICE_INFO_HEADER
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 타입
            /// </summary>
            public DISPLAYCONFIG_DEVICE_INFO_TYPE Type;

            /// <summary>
            /// 크기
            /// </summary>
            public uint Size;

            /// <summary>
            /// 어댑터 ID
            /// </summary>
            public LUID AdapterID;

            /// <summary>
            /// ID
            /// </summary>
            public uint ID;

            #endregion
        }

        #endregion
        #region 디스플레이 구성 / 타겟 장치명 - DISPLAYCONFIG_TARGET_DEVICE_NAME

        /// <summary>
        /// 디스플레이 구성 / 타겟 장치명
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DISPLAYCONFIG_TARGET_DEVICE_NAME
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////// Field
            ////////////////////////////////////////////////////////////////////////////////////////// Public

            #region Field

            /// <summary>
            /// 헤더
            /// </summary>
            public DISPLAYCONFIG_DEVICE_INFO_HEADER Header;

            /// <summary>
            /// 플래그
            /// </summary>
            public DISPLAYCONFIG_TARGET_DEVICE_NAME_FLAG Flag;

            /// <summary>
            /// 출력 기술
            /// </summary>
            public DISPLAYCONFIG_VIDEO_OUTPUT_TECHNOLOGY OutputTechnology;

            /// <summary>
            /// EDID 제조자 ID
            /// </summary>
            public ushort EDIDManufactureID;

            /// <summary>
            /// EDID 제품 코드 ID
            /// </summary>
            public ushort EDIDProductCodeID;

            /// <summary>
            /// 연결자 인스턴스
            /// </summary>
            public uint ConnectorInstance;

            /// <summary>
            /// 모니터 친화적 장치명
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string MonitorFriendlyDeviceName;

            /// <summary>
            /// 모니터 장치 경로
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string MonitorDevicePath;

            #endregion
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Import
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Private

        #region 디스플레이 구성 / 버퍼 크기 구하기 - GetDisplayConfigBufferSizes(flag, pathInfoArrayElementCount, modeInfoArrayElementCount)

        /// <summary>
        /// 디스플레이 구성 / 버퍼 크기 구하기
        /// </summary>
        /// <param name="flag">플래그</param>
        /// <param name="pathInfoArrayElementCount">경로 정보 배열 요소 카운트</param>
        /// <param name="modeInfoArrayElementCount">모드 정보 배열 요소 카운트</param>
        /// <returns>처리 결과</returns>
        [DllImport("user32")]
        private static extern int GetDisplayConfigBufferSizes
        (
            QUERY_DEVICE_CONFIG_FLAG flag,
            out uint pathInfoArrayElementCount,
            out uint modeInfoArrayElementCount
        );

        #endregion
        #region 디스플레이 구성 질의하기 - QueryDisplayConfig(flag, pathInfoArrayElementCount, pathInfoArray, modeInfoArrayElementCount, modeInfoArray, currentTopologyID)

        /// <summary>
        /// 디스플레이 구성 질의하기
        /// </summary>
        /// <param name="flag">플래그</param>
        /// <param name="pathInfoArrayElementCount">경로 배열 요소 카운트</param>
        /// <param name="pathInfoArray">경로 정보 배열</param>
        /// <param name="modeInfoArrayElementCount">모드 정보 배열 요소 카운트</param>
        /// <param name="modeInfoArray">모드 정보 배열</param>
        /// <param name="currentTopologyID">현재 토폴로지 ID</param>
        /// <returns>처리 결과</returns>
        [DllImport("user32")]
        private static extern int QueryDisplayConfig
        (
            QUERY_DEVICE_CONFIG_FLAG flag,
            ref uint pathInfoArrayElementCount,
            [Out] DISPLAYCONFIG_PATH_INFO[] pathInfoArray,
            ref uint modeInfoArrayElementCount,
            [Out] DISPLAYCONFIG_MODE_INFO[] modeInfoArray,
            IntPtr currentTopologyID
        );

        #endregion
        #region 디스플레이 구성 / 장치 정보 구하기 - DisplayConfigGetDeviceInfo(deviceName)

        /// <summary>
        /// 디스플레이 구성 / 장치 정보 구하기
        /// </summary>
        /// <param name="deviceName">장치명</param>
        /// <returns>처리 결과</returns>
        [DllImport("user32")]
        private static extern int DisplayConfigGetDeviceInfo(ref DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName);

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Field
        ////////////////////////////////////////////////////////////////////////////////////////// Public

        #region Field

        /// <summary>
        /// ERROR_SUCCESS
        /// </summary>
        public const int ERROR_SUCCESS = 0;

        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////////////// Method
        ////////////////////////////////////////////////////////////////////////////////////////// Static
        //////////////////////////////////////////////////////////////////////////////// Public

        #region 친화적 장치명 구하기 - GetFriendlyDeviceName(screen)

        /// <summary>
        /// 친화적 장치명 구하기
        /// </summary>
        /// <param name="screen">화면</param>
        /// <returns>친화적 장치명</returns>
        public static string GetFriendlyDeviceName(Screen screen)
        {
            IEnumerable<string> enumerable = GetAllMonitorFriendlyDeviceNameEnumerable();

            for (int i = 0; i < Screen.AllScreens.Length; i++)
            {
                if (Equals(screen, Screen.AllScreens[i]))
                {
                    return enumerable.ToArray()[i];
                }
            }

            return null;
        }

        #endregion

        //////////////////////////////////////////////////////////////////////////////// Private

        #region 모니터 친화적 장치명 구하기 - GetMonitorFriendlyDeviceName(adapterID, targetID)

        /// <summary>
        /// 모니터 친화적 장치명 구하기
        /// </summary>
        /// <param name="adapterID">어댑터 ID</param>
        /// <param name="targetID">타겟 ID</param>
        /// <returns>모니터 친화적 장치명</returns>
        private static string GetMonitorFriendlyDeviceName(LUID adapterID, uint targetID)
        {
            DISPLAYCONFIG_TARGET_DEVICE_NAME deviceName = new DISPLAYCONFIG_TARGET_DEVICE_NAME
            {
                Header =
            {
                Size      = (uint)Marshal.SizeOf(typeof (DISPLAYCONFIG_TARGET_DEVICE_NAME)),
                AdapterID = adapterID,
                ID        = targetID,
                Type      = DISPLAYCONFIG_DEVICE_INFO_TYPE.DISPLAYCONFIG_DEVICE_INFO_GET_TARGET_NAME
            }
            };

            int error = DisplayConfigGetDeviceInfo(ref deviceName);

            if (error != ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            return deviceName.MonitorFriendlyDeviceName;
        }

        #endregion
        #region 모든 모니터 친화적 장치명 열거 가능형 구하기 - GetAllMonitorFriendlyDeviceNameEnumerable()

        /// <summary>
        /// 모든 모니터 친화적 장치명 열거 가능형 구하기
        /// </summary>
        /// <returns>모든 모니터 친화적 장치명 열거 가능형</returns>
        private static IEnumerable<string> GetAllMonitorFriendlyDeviceNameEnumerable()
        {
            uint pathCount;
            uint modeCount;

            int error = GetDisplayConfigBufferSizes
            (
                QUERY_DEVICE_CONFIG_FLAG.QDC_ONLY_ACTIVE_PATHS,
                out pathCount,
                out modeCount
            );

            if (error != ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            DISPLAYCONFIG_PATH_INFO[] pathInfoArray = new DISPLAYCONFIG_PATH_INFO[pathCount];
            DISPLAYCONFIG_MODE_INFO[] modeInfoArray = new DISPLAYCONFIG_MODE_INFO[modeCount];

            error = QueryDisplayConfig
            (
                QUERY_DEVICE_CONFIG_FLAG.QDC_ONLY_ACTIVE_PATHS,
                ref pathCount,
                pathInfoArray,
                ref modeCount,
                modeInfoArray,
                IntPtr.Zero
            );

            if (error != ERROR_SUCCESS)
            {
                throw new Win32Exception(error);
            }

            for (int i = 0; i < modeCount; i++)
            {
                if (modeInfoArray[i].InfoType == DISPLAYCONFIG_MODE_INFO_TYPE.DISPLAYCONFIG_MODE_INFO_TYPE_TARGET)
                {
                    yield return GetMonitorFriendlyDeviceName(modeInfoArray[i].AdapterID, modeInfoArray[i].ID);
                }
            }
        }

        #endregion
    }
}
