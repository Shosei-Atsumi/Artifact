## -*- coding: utf-8 -*-
import cv2
import numpy as np
import random

def main():
    cv2_image = cv2.imread('cap.png')
    
    # コントラスト、明るさを変更する。
    cv2_image = adjust(cv2_image, alpha=4.5, beta=-200)

    gray = cv2.cvtColor(cv2_image, cv2.COLOR_BGR2GRAY)
    cv2.imwrite('gray.jpg', gray)

    edge = cv2.Canny(gray, 50, 200)
    cv2.imwrite('edge.jpg', edge)

    kernel = cv2.getStructuringElement(cv2.MORPH_RECT, (2, 2))
    edge2 = cv2.dilate(edge, kernel)
    cv2.imwrite('edge2.jpg', edge2)

    contours, hierarchy = cv2.findContours(edge2, cv2.RETR_TREE, cv2.CHAIN_APPROX_SIMPLE)

    curves = []
    for contour, hierarchy in zip(contours, hierarchy[0]):
        curve = cv2.approxPolyDP(contour, 0.01*cv2.arcLength(contour, True), True)
        if len(curve) == 4:
            curves.append(curve)
    curves = sorted( curves, key=lambda x: (x.ravel()[1], x.ravel()[0]) )

    rect_image = cv2_image.copy()
    for i, curve in enumerate(curves):
        p1, p3 = curve[0][0], curve[2][0]
        x1, y1, x2, y2 = p1[0], p1[1], p3[0], p3[1]
        r, g, b = random.random()*255, random.random()*255, random.random()*255
        cv2.rectangle(rect_image, (x1, y1), (x2, y2), (b, g, r), thickness=2)
    cv2.imwrite('rect_image.jpg', rect_image)

def adjust(img, alpha=1.0, beta=0.0):
    # 積和演算を行う。
    dst = alpha * img + beta
    # [0, 255] でクリップし、uint8 型にする。
    return np.clip(dst, 0, 255).astype(np.uint8)

if __name__ == "__main__":
    main()