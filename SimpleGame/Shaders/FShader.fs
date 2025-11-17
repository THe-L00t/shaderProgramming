#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform sampler2D u_RGBTesture;
uniform float u_Time;

in vec2 vTexPos;

const float c_PI = 3.141592;

void main()
{
    // 가우시안 블러 효과
    // 텍스처 크기 (픽셀 단위로 샘플링하기 위해)
    vec2 texelSize = vec2(2.0 / 512.0, 1.0 / 512.0); // 텍스처 크기에 맞게 조정

    // 5x5 가우시안 커널 가중치
    float kernel[25];
    kernel[0] = 1.0;  kernel[1] = 4.0;  kernel[2] = 7.0;  kernel[3] = 4.0;  kernel[4] = 1.0;
    kernel[5] = 4.0;  kernel[6] = 16.0; kernel[7] = 26.0; kernel[8] = 16.0; kernel[9] = 4.0;
    kernel[10] = 7.0; kernel[11] = 26.0; kernel[12] = 41.0; kernel[13] = 26.0; kernel[14] = 7.0;
    kernel[15] = 4.0; kernel[16] = 16.0; kernel[17] = 26.0; kernel[18] = 16.0; kernel[19] = 4.0;
    kernel[20] = 1.0; kernel[21] = 4.0;  kernel[22] = 7.0;  kernel[23] = 4.0;  kernel[24] = 1.0;

    float kernelSum = 273.0; // 모든 가중치의 합

    vec4 blurredColor = vec4(0.0);

    // 5x5 영역 샘플링
    for(int i = 0; i < 5; i++)
    {
        for(int j = 0; j < 5; j++)
        {
            vec2 offset = vec2(float(i - 2), float(j - 2)) * texelSize;
            vec2 sampleUV = v_UV + offset;

            int kernelIndex = i * 5 + j;
            blurredColor += texture(u_RGBTesture, sampleUV) * kernel[kernelIndex];
        }
    }

    // 가중치 합으로 정규화
    blurredColor /= kernelSum;

    FragColor = blurredColor;
}
