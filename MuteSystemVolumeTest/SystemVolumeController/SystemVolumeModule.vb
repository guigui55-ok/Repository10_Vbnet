Imports NAudio.CoreAudioApi
Module SystemVolumeModule

    Public Class VolumeControl
        Private ReadOnly _deviceEnumerator As MMDeviceEnumerator
        Private ReadOnly _defaultDevice As MMDevice

        Public Sub New()
            _deviceEnumerator = New MMDeviceEnumerator()
            _defaultDevice = _deviceEnumerator.GetDefaultAudioEndpoint(Dataflow.Render, Role.Multimedia)
        End Sub

        ''' <summary>
        ''' システムボリュームを取得します。（0.0〜1.0の範囲）
        ''' </summary>
        Public Function GetVolume() As Single
            Return _defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar
        End Function

        ''' <summary>
        ''' システムボリュームを設定します。（0.0〜1.0の範囲）
        ''' </summary>
        Public Sub SetVolume(ByVal volume As Single)
            _defaultDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volume
        End Sub

        Public Function IsInvalidVolume(ByVal volume As Single) As Boolean
            If 0 <= volume And volume < 1.0 Then
                Return False
            Else
                Return True
            End If
        End Function

        ''' <summary>
        ''' ミュート状態を取得します。
        ''' </summary>
        Public Function IsMuted() As Boolean
            Return _defaultDevice.AudioEndpointVolume.Mute
        End Function

        ''' <summary>
        ''' ミュート状態を切り替えます。
        ''' </summary>
        Public Sub ToggleMute()
            _defaultDevice.AudioEndpointVolume.Mute = Not _defaultDevice.AudioEndpointVolume.Mute
        End Sub


    End Class
End Module
