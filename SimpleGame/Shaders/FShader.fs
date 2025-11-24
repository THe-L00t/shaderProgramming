#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform sampler2D u_RGBTesture;
uniform sampler2D u_DigitTexture;
uniform sampler2D u_NumTexture;
uniform float u_Time;

in vec2 vTexPos;

const float c_PI = 3.141592;

void test()
{
	vec2 newUV = v_UV;
    float dx = 0.1 * sin(newUV.y *2*c_PI - u_Time);
    float dy = 0.1 * sin(newUV.x *2*c_PI - u_Time);
    newUV += vec2(dx,dy);
    vec4 newColor = texture(u_RGBTesture, newUV);
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.02,newUV.y));
    newColor += texture(u_RGBTesture, vec2(newUV.x-0.04,newUV.y));
    newColor /=3;
    FragColor = newColor;

}

void Circle(){
	vec2 newUV = v_UV;//0~1
	vec2 center = vec2(0.5,0.5);
	float d = distance(newUV, center);
	vec4 newColor = vec4(0);

    float value = sin(d*4*c_PI- u_Time);
    newColor = vec4(value);

	FragColor = newColor;
}

void Flag()
{
    vec2 newUV = vec2(v_UV.x,1-v_UV.y-0.5);
    vec4 newColor = vec4(0);

    float width = 0.2;
    float sinValue = v_UV.x*0.2*sin(newUV.x*2*c_PI - u_Time);

    if(newUV.y < sinValue + width && newUV.y > sinValue - width){
        newColor = vec4(1);
    }
    else 
    {
        discard;
    }
    FragColor = newColor;
}

void q1()
{
    vec2 newUV = vec2(v_UV.x,v_UV.y);
    float x = newUV.x;
    float y = 1-abs(2*(v_UV.y -0.5));
    vec4 newColor = texture(u_RGBTesture, vec2(x,y));

    FragColor = newColor;
}
void q2()
{
    vec2 newUV = vec2(v_UV.x,v_UV.y);
    float x = fract(newUV.x*3);
    float y = (2-floor(newUV.x*3))/3 + v_UV.y/3;
    vec4 newColor = texture(u_RGBTesture, vec2(x,y));

    FragColor = newColor;
}

void Brick1()
{
    vec2 newUV = vec2(v_UV.x,v_UV.y);
    float rCount = 3;
    float sAmount = 0.2;

    float x = fract(newUV.x*rCount) +floor(newUV.y*rCount)*sAmount;
    float y = fract(newUV.y*rCount);
    vec4 newColor = texture(u_RGBTesture, vec2(x,y));

    FragColor = newColor;
}

void Brick2()
{
    vec2 newUV = vec2(v_UV.x,v_UV.y);
    float x = fract(newUV.x*2);
    float y = fract(newUV.y*2)+floor(newUV.x*2)*0.5;
    vec4 newColor = texture(u_RGBTesture, vec2(x,y));

    FragColor = newColor;
}

void AI()
{
    // 1. 초기 UV 설정 및 애니메이션 웨이브 변형
    vec2 newUV = v_UV;
    
    // 시간과 UV에 따라 변화하는 두 개의 사인파를 이용해 UV 좌표를 변형합니다.
    // 이는 텍스처가 물결치듯 일렁이는 효과를 줍니다.
    // 진폭: 0.05, 주파수: x, y축으로 4배
    float waveX = 0.05 * sin(newUV.y * 4.0 * c_PI + u_Time * 2.0);
    float waveY = 0.05 * cos(newUV.x * 4.0 * c_PI - u_Time * 1.5);
    
    newUV.x += waveX;
    newUV.y += waveY;
    
    // 2. 텍스처 샘플링 및 색상 보정
    // 변형된 UV 좌표로 텍스처를 샘플링합니다.
    vec4 texColor = texture(u_RGBTesture, newUV);
    
    // 3. 리플(Ripple) 이펙트 추가 (동심원)
    vec2 center = vec2(0.5, 0.5);
    float d = distance(v_UV, center); // 원점과의 거리 (0.0 ~ 0.707)
    
    // 거리에 따라 사인파를 생성하여 리플 효과를 만듭니다.
    // 주파수: 10배, 속도: 3배
    float ripple = sin(d * 10.0 * c_PI - u_Time * 3.0);
    
    // 4. 색상 스펙트럼(Hue Shift) 및 강조
    // ripple 값을 기반으로 색상을 변화시켜 화려함을 더합니다.
    // 0.5를 더해 음수 값을 없애고 0~1 범위로 만듭니다.
    float colorShift = ripple * 0.5 + 0.5;
    
    // R, G, B 채널에 서로 다른 위상차를 갖는 파동을 적용하여 무지개 빛 효과를 줍니다.
    float r = sin(u_Time * 1.0 + colorShift * c_PI * 2.0) * 0.5 + 0.5;
    float g = sin(u_Time * 1.0 + colorShift * c_PI * 2.0 + 2.0 * c_PI / 3.0) * 0.5 + 0.5;
    float b = sin(u_Time * 1.0 + colorShift * c_PI * 2.0 + 4.0 * c_PI / 3.0) * 0.5 + 0.5;
    
    vec3 spectrum = vec3(r, g, b);

    // 5. 최종 색상 조합
    // 텍스처 색상에 스펙트럼 색상을 곱하여(Modulate) 텍스처의 디테일을 유지하면서 화려한 색상을 입힙니다.
    vec4 finalColor = texColor * vec4(spectrum, 1.0);
    
    // ripple 값이 강한 부분(파동의 정점)을 밝게 강조하여 더욱 다이나믹하게 만듭니다.
    finalColor.rgb *= (1.0 + abs(ripple) * 0.5); 
    
    FragColor = finalColor;
}

void Glitch_Effect()
{
    vec2 newUV = v_UV;
    vec2 offsetUV = newUV;
    
    // 1. 시간 기반의 무작위 수열(Pseudo-Random) 생성
    // fract(sin(dot(...))) 패턴을 사용하여 u_Time에 따라 불규칙하게 변화하는 값 생성
    float noise = fract(sin(dot(newUV, vec2(12.9898, 78.233))) * 43758.5453 + u_Time * 10.0);
    
    // 2. 수평 왜곡 (Scanline Distortion)
    // 노이즈와 시간, UV의 y축 위치를 기반으로 수평 왜곡 오프셋을 계산합니다.
    float wave_strength = 0.05 + noise * 0.1; // 노이즈에 따라 왜곡 강도 변화
    float wave_offset = sin(newUV.y * 50.0 + u_Time * 20.0 * noise) * wave_strength;
    
    // 왜곡이 심할 때는 수직으로도 흔들리게 합니다.
    float vertical_shift = step(0.95, noise) * 0.05 * sin(u_Time * 50.0);
    
    offsetUV.x += wave_offset;
    offsetUV.y += vertical_shift;
    
    // 3. 색상 분리 (Chromatic Aberration)
    // 각 R, G, B 채널을 서로 다른 오프셋으로 샘플링하여 색수차 효과를 만듭니다.
    float glitch_factor = 0.005 + noise * 0.01; // 글리치 강도에 따라 분리 정도 변화
    
    vec2 redUV   = newUV + vec2(glitch_factor, 0.0) * wave_offset;
    vec2 greenUV = newUV + vec2(0.0, 0.0); // Green 채널은 중앙
    vec2 blueUV  = newUV - vec2(glitch_factor, 0.0) * wave_offset;
    
    // 최종 UV는 R, G, B 채널에 개별적으로 적용됩니다.
    
    // 4. 최종 색상 샘플링 및 조합
    // 수평 왜곡된 UV와 색상 분리 오프셋을 결합하여 샘플링합니다.
    vec4 color;
    color.r = texture(u_RGBTesture, redUV + vec2(wave_offset, vertical_shift)).r;
    color.g = texture(u_RGBTesture, greenUV + vec2(wave_offset, vertical_shift)).g;
    color.b = texture(u_RGBTesture, blueUV + vec2(wave_offset, vertical_shift)).b;
    color.a = 1.0;
    
    // 5. 스캔 라인 비네팅 (Scanline Vignetting)
    // 가장자리를 약간 어둡게 하고 중간중간 깜빡이는 라인을 추가하여 TV 화면 느낌을 더합니다.
    float vignette = smoothstep(0.0, 0.5, 1.0 - distance(newUV, vec2(0.5)));
    color.rgb *= vignette * 0.5 + 0.5; // 비네팅

    // 깜빡이는 스캔 라인 효과
    float scanline = sin(newUV.y * 300.0) * 0.05 + 0.95;
    color.rgb *= mix(scanline, 1.0, 0.7); // 70%는 일반 밝기, 30%는 스캔라인 적용

    FragColor = color;
}

void digit()
{
    vec2 newUV = v_UV;

    int digit = int(u_Time)%10;
    int titleIndex = (digit + 9)%10;

    float offX = ((titleIndex) % 5) * 0.2;
    float offY = floor(titleIndex/5.f) * 0.5;

    newUV.x = newUV.x*0.2 + offX;
    newUV.y = newUV.y*0.5 + offY;
    FragColor = texture(u_NumTexture, newUV);
}

void AI_cnt()
{
    vec2 newUV = v_UV; 
    
    // 1. 카운터 및 자릿수 분리
    int counter = int(u_Time) % 100;
    int ones_digit = counter % 10;
    int tens_digit = counter / 10; 
    
    
    // 2. 출력 모드 결정 및 텍스처 인덱스 및 샘플링 UV 설정
    
    int textureIndex;
    vec2 sampledUV; // 이 UV는 0.0 ~ 1.0 범위로 정규화될 것임
    
    
    if (counter < 10) 
    {
        // 📌 A. 한 자릿수 모드 (0 ~ 9): 화면 전체에 크게 출력
        
        // 오리지널 역순환 인덱스 로직 사용 (u_Time=0 -> Index 9, u_Time=1 -> Index 0)
        textureIndex = (ones_digit + 9) % 10; 
        
        // **수정된 부분:** 화면 전체 UV (newUV)를 그대로 사용합니다. 
        // 이 UV는 다음 단계에서 텍스처 칸 크기로 축소됩니다.
        sampledUV = newUV; 
        
    } 
    else 
    {
        // 📌 B. 두 자릿수 모드 (10 ~ 99): 화면을 반으로 나누어 출력
        
        // 0: 십의 자리, 1: 일의 자리
        int digit_index = int(floor(newUV.x * 2.0)); 
        
        // 현재 픽셀이 표시해야 할 숫자 값
        int current_digit = (digit_index == 0) ? tens_digit : ones_digit;
        
        textureIndex = current_digit;
        
        // **수정된 부분:** 현재 자릿수의 화면 영역 내에서 UV 좌표를 0.0 ~ 1.0으로 정규화
        sampledUV.x = fract(newUV.x * 2.0); 
        sampledUV.y = newUV.y;
    }
    
    
    // 3. 텍스처 오프셋 정확히 계산 (5x2 배열 가정)
    
    float col_index = float(textureIndex % 5); 
    float offX = col_index * 0.2; 
    
    float row_index = floor(float(textureIndex) / 5.0); 
    float offY = row_index * 0.5;
    
    
    // 4. 최종 UV 좌표 조정 및 샘플링 (핵심: sampledUV를 텍스처 칸 크기로 축소)
    
    vec2 finalUV;
    
    // sampledUV (0~1)를 텍스처 한 칸 크기(0.2x0.5)로 축소 후, 오프셋을 더함
    finalUV.x = sampledUV.x * 0.2 + offX;
    finalUV.y = sampledUV.y * 0.5 + offY;

    // 만약 한 자릿수 모드에서 0~9 대신 1~10을 카운트해야 한다면, 
    // 여기서 (textureIndex == 0)일 때 discard 또는 투명 처리를 해야 하지만, 
    // 요청하신 대로 0부터 카운트하므로 생략합니다.
    
    FragColor = texture(u_RGBTesture, finalUV);
}

void main()
{
    AI_cnt();
}
