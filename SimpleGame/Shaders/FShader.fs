#version 330

layout(location=0) out vec4 FragColor;

in vec2 v_UV;

uniform float u_Time;

// 랜덤 함수 (노이즈 생성용)
float random(vec2 st) {
    return fract(sin(dot(st.xy, vec2(12.9898,78.233))) * 43758.5453123);
}

// 2D 노이즈 함수
float noise(vec2 st) {
    vec2 i = floor(st);
    vec2 f = fract(st);
    float a = random(i);
    float b = random(i + vec2(1.0, 0.0));
    float c = random(i + vec2(0.0, 1.0));
    float d = random(i + vec2(1.0, 1.0));
    vec2 u = f * f * (3.0 - 2.0 * f);
    return mix(a, b, u.x) + (c - a)* u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}

// 비네트 효과
float vignette(vec2 uv, float intensity, float extent) {
    uv = uv * 2.0 - 1.0;
    float dist = length(uv);
    return 1.0 - smoothstep(extent, intensity, dist);
}

// 펄스 효과 (심장박동)
float pulse(float time) {
    float heartbeat = sin(time * 2.0) * 0.5 + 0.5;
    heartbeat = pow(heartbeat, 3.0);
    return heartbeat * 0.3 + 0.7;
}

void main()
{
    vec2 uv = v_UV * 0.5 + 0.5; // -1~1 범위를 0~1로 변환

    // 시간 기반 효과
    float time = u_Time * 0.001; // 밀리초를 초로 변환

    // 1. 크로마틱 애버레이션 (RGB 색상 분리)
    float aberration = 0.015 * sin(time * 0.5);
    vec2 offsetR = vec2(aberration, 0.0);
    vec2 offsetB = vec2(-aberration, 0.0);

    float r = uv.x + offsetR.x;
    float g = uv.y;
    float b = uv.x + offsetB.x;

    // 2. 글리치 효과 (간헐적 화면 왜곡)
    float glitchLine = step(0.98, random(vec2(time * 0.1, floor(uv.y * 20.0))));
    float glitchStrength = glitchLine * random(vec2(time)) * 0.1;
    uv.x += glitchStrength;

    // 3. 펄스 효과
    float heartbeat = pulse(time);

    // 4. 비네트 효과 (화면 가장자리 어둡게)
    float vig = vignette(uv, 1.2, 0.5);
    vig *= heartbeat; // 펄스와 연동

    // 5. 노이즈/그레인 효과
    float grain = noise(uv * 500.0 + time * 50.0) * 0.15;

    // 6. 색상 왜곡 (불길한 색감)
    vec3 baseColor = vec3(r, g * 0.7, b * 0.6); // 녹색과 파란색 억제

    // 붉은 색조 추가 (공포 분위기)
    float redTint = sin(time * 0.3) * 0.2 + 0.3;
    baseColor.r += redTint * (1.0 - vig);

    // 7. 스캔라인 효과
    float scanline = sin(uv.y * 800.0 + time * 10.0) * 0.04;

    // 8. 간헐적 플래시 효과
    float flash = step(0.995, random(vec2(time * 0.5))) * 0.5;

    // 최종 색상 조합
    vec3 finalColor = baseColor;
    finalColor *= vig; // 비네트 적용
    finalColor += grain; // 노이즈 추가
    finalColor += scanline; // 스캔라인 추가
    finalColor += flash; // 플래시 추가

    // 어두운 톤으로 조정
    finalColor *= 0.8;

    // 대비 증가
    finalColor = pow(finalColor, vec3(1.2));

    FragColor = vec4(finalColor, 1.0);
}
