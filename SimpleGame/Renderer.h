#pragma once

#include <string>
#include <cstdlib>
#include <fstream>
#include <iostream>
#include <cassert>
#include "Dependencies\glew.h"
#include "LoadPng.h"

class Renderer
{
public:
	Renderer(int windowSizeX, int windowSizeY);
	~Renderer();

	bool IsInitialized();
	void ReloadAllShaderPrograms();
	void DrawSolidRect(float x, float y, float z, float size, float r, float g, float b, float a);
	void DrawTest();
	void DrawParticle();
	void DrawGridMexh();
	void DrawFullScreen(float r, float g, float b, float a);
	void DrawFS();
	void DrawTexture();

private:
	void Initialize(int windowSizeX, int windowSizeY);
	bool ReadFile(char* filename, std::string* target);
	void AddShader(GLuint ShaderProgram, const char* pShaderText, GLenum ShaderType);
	GLuint CompileShaders(char* filenameVS, char* filenameFS);
	void CreateVertexBufferObjects();
	void GetGLPosition(float x, float y, float* newX, float* newY);
	void GeneralParticles(int numParticle);
	void CompileAllShaderPrograms();
	void DeleteAllShaderPrograms();
	void CreateGridMesh(int x, int y);
	GLuint CreatePngTexture(char* filePath, GLuint samplingMethod);

	bool m_Initialized = false;

	unsigned int m_WindowSizeX = 0;
	unsigned int m_WindowSizeY = 0;

	GLuint m_VBORect = 0;
	GLuint m_SolidRectShader = 0;

	GLuint m_VBOtestPos = 0;
	GLuint m_VBOtestColor = 0;
	GLuint m_TestShader = 0;

	//Time
	float m_time = 0;

	//Particle
	GLuint m_ParticleShader = 0;
	GLuint m_VBOPraticle = 0;
	GLuint m_VBOPraticleVertexCount = 0;


	//Grid
	GLuint m_GridMeshShader{};
	GLuint m_GridMeshVBO{};
	GLuint m_GridMeshVertexCount{};

	//full screen 
	GLuint m_VBOFullScreen{};
	GLuint m_FullScreenShader{};


	//for raindrop effect
	float m_Points[100 * 4];

	//for fragment shader factory
	GLuint m_VBOFS = 0;
	GLuint m_FS = 0;

	GLuint m_RGBTexture{};

	GLuint m_0Texture{};
	GLuint m_1Texture{};
	GLuint m_2Texture{};
	GLuint m_3Texture{};
	GLuint m_4Texture{};
	GLuint m_5Texture{};
	GLuint m_6Texture{};
	GLuint m_7Texture{};
	GLuint m_8Texture{};
	GLuint m_9Texture{};
	GLuint m_NumTexture{};

	//Texture
	GLuint m_TexVBO{};
	GLuint m_TexShader{};
};

